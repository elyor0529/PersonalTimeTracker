package datastore
import (
	"log"
	"errors"
	"time"
)
type SharedTask struct {
	EmailFrom string
	Name string
	TimeSpent float64
	Date string
	ConvertedDate time.Time
	EmailTo string
}

type SharedTaskFromDb struct{
	TaskName string
	TimeSpent float64
	TaskDate time.Time
	EmailFrom string
}

var insertSharedTaskString = "INSERT INTO TaskSharing (emailFrom, taskId, emailTo) values ($1, $2, $3);"
var selectSharedTasksByEmailToString = "select t.taskname, t.timespent, t.taskDate, tasksharing.emailfrom from task as t join tasksharing on tasksharing.taskid = t.taskid where tasksharing.emailto = $1"

var selectCountSharedTasksByEmailTo = "Select Count(*) as count from Tasksharing where emailTo = $1"

var selectTasksByNameDateDuration  = "select taskid from task where taskname = $1 and taskDate = $2 and timespent = $3";

var selectTasksByTaskIdEmailToFrom = "select * from tasksharing where taskid = $1 and emailfrom = $2 and emailto = $3";

func MakeSharedTask( emailFrom *string, name *string, duration float64, taskDateInput *string, emailTo *string)  *SharedTask {
	convertedTaskDate, err := getRfc3339Time(*taskDateInput)
	if err != nil {
		return nil
	}
	task := new(SharedTask)
	task.ConvertedDate = convertedTaskDate

	task.EmailFrom = *emailFrom
	task.Name = *name
	task.TimeSpent = duration
	task.EmailTo = *emailTo
	task.Date = *taskDateInput
	return task
}

func AddSharedTask( task *SharedTask, taskId string) error {
	log.Println("TaskId " + taskId)
	_, stmterr := globalOrm.orm.Exec(insertSharedTaskString, task.EmailFrom, taskId, task.EmailTo);
	if stmterr != nil {
		log.Println("statement error : " + stmterr.Error())
	}
	return stmterr
}

func SelectSharedTasksByEmailTo( emailTo string ) ([](SharedTaskFromDb), error) {
	countRows, counterr := globalOrm.orm.Query(selectCountSharedTasksByEmailTo, emailTo)
	if (counterr != nil) {
		log.Println("statement error : " + counterr.Error())
		return nil, counterr
	}
	var rowsCount int
	for countRows.Next() {
		countRows.Scan(&rowsCount)
	}
	countRows.Scan(&rowsCount)
	rows, stmterr := globalOrm.orm.Query(selectSharedTasksByEmailToString, emailTo)
	if stmterr != nil {
		log.Println("statement error : " + stmterr.Error())
			return nil, stmterr
	} else {
			//log.Printf("rowsCount for GetAllTasks is %d\n", rowsCount)
			taskMap := make( [](SharedTaskFromDb), rowsCount)
			taskMapCounter := 0
			var taskName string
			var timeSpent float64
			var taskDate time.Time
			var emailFrom string
			var taskFromDbPtr *SharedTaskFromDb
			for rows.Next()  {
				rows.Scan( &taskName, &timeSpent, &taskDate, &emailFrom)
				taskFromDbPtr = new(SharedTaskFromDb)
				taskMap[taskMapCounter] = *taskFromDbPtr 
				taskMap[taskMapCounter].TaskName = taskName
				taskMap[taskMapCounter].TimeSpent = timeSpent
				taskMap[taskMapCounter].TaskDate = taskDate
				taskMap[taskMapCounter].EmailFrom = emailFrom
				taskMapCounter++
			}
			return taskMap[:], nil
		}
}

func ExistSharedTask( sharedTask *SharedTask) (string, error, string){
  rows, stmterr := globalOrm.orm.Query(selectTasksByNameDateDuration, sharedTask.Name, sharedTask.ConvertedDate, sharedTask.TimeSpent);
	if stmterr != nil {
		log.Println("statement error : " + stmterr.Error())
			return "", stmterr, "Internal Server error"
	} else {
		rowsNext := rows.Next()
		var taskId string
		if rowsNext == false {
			return "", errors.New("This task does not exist."), "This task does not exist."
		} else {
			rows.Scan(&taskId)
			//log.Println("taskID read: " + taskId)
			rows, stmterr = globalOrm.orm.Query(selectTasksByTaskIdEmailToFrom, taskId, sharedTask.EmailFrom, sharedTask.EmailTo);
			if stmterr != nil {
				log.Println("statement error : " + stmterr.Error())
			} else {
				rowsNext = rows.Next()
				if rowsNext == false {
					stmterr = nil
					return taskId, nil, ""
				} else {
					return "", errors.New("This task is already shared to the other account."), "This task is already shared to the other account."
				}
			}
			
		}
	}
	return "", stmterr, ""
}

func getRfc3339Time(datetime string ) (time.Time, error){
	convertedTaskDate, err := time.Parse(time.RFC3339, datetime)
	if err == nil {
		return convertedTaskDate, nil
	} else {
		log.Println("time conversion error: " + err.Error())
		return time.Time{}, err
	}
}
