package datastore
import (
	"log"
//	"errors"
	"time"
)
type Task struct {
  taskId []byte
  taskName string
  timeSpent float64
	taskDate string
	email string
}

type TaskFromDb struct{
	TaskName string
	TimeSpent float64
	TaskDate time.Time
}

var insertTaskString = "INSERT INTO Task (taskId, taskName, timeSpent, taskDate, email) values (uuid_generate_v4(),$1, $2, $3, $4);"
var selectTasksByEmailString = "Select taskname, timespent, taskDate from Task where email = $1"
var selectCountTasksByEmail = "Select Count(*) as count from Task where email = $1"

func MakeTask( name *string, duration *float64, email *string, taskDateInput *string)  *Task {
	task := new(Task)
	task.taskName = *name
	task.timeSpent = *duration
	task.email = *email
	task.taskDate = *taskDateInput
	return task
}

func AddTask( task *Task) error {
	convertedTaskDate, err := time.Parse(time.RFC3339, task.taskDate)
	if err != nil {
		log.Println("DateTime conversion error: " + err.Error())
		return err
	}
	_, stmterr := globalOrm.orm.Exec(insertTaskString, task.taskName, task.timeSpent, convertedTaskDate, task.email);
	if stmterr != nil {
		log.Println("statement error : " + stmterr.Error())
	}
	return stmterr
}

func SelectTasksByEmail( email string ) ([](TaskFromDb), error) {
	//log.Printf("email is %s\n", email)
	countRows, counterr := globalOrm.orm.Query(selectCountTasksByEmail, email)
	if (counterr != nil) {
		log.Println("statement error : " + counterr.Error())
		return nil, counterr
	}
	var rowsCount int
	//var rowsCountStr string
	for countRows.Next() {
		countRows.Scan(&rowsCount)
	}
	//log.Printf("rowsCount %d", rowsCount)
	/*if countRows.Next() {
		
	} else {
		log.Println("Zero rows.")
		return nil, errors.New("No rows")
	}*/
	countRows.Scan(&rowsCount)
	//log.Printf("rowsCount %d", rowsCount)
	rows, stmterr := globalOrm.orm.Query(selectTasksByEmailString, email)
	if stmterr != nil {
		log.Println("statement error : " + stmterr.Error())
			return nil, stmterr
	} else {
			//log.Printf("rowsCount for GetAllTasks is %d\n", rowsCount)
			taskMap := make( [](TaskFromDb), rowsCount)
			taskMapCounter := 0
			var taskName string
			var timeSpent float64
			var taskDate time.Time
			var taskFromDbPtr *TaskFromDb
			for rows.Next()  {
				rows.Scan( &taskName, &timeSpent, &taskDate)
				taskFromDbPtr = new(TaskFromDb)
				taskMap[taskMapCounter] = *taskFromDbPtr 
				taskMap[taskMapCounter].TaskName = taskName
				taskMap[taskMapCounter].TimeSpent = timeSpent
				taskMap[taskMapCounter].TaskDate = taskDate
				taskMapCounter++
			}
			return taskMap[:], nil
		}
}
