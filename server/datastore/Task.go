package datastore
import (
)
type Task struct {
  taskId int
  taskName string
  timeSpent float64
	email string
}

var insertTaskString = "INSERT INTO Task (taskName, timeSpent, email) values ($1, $2, $3);"
var selectTasksByEmailString = "Select taskname, timespent from Task where email = $1"
var selectCountTasksByEmail = "Select Count(*) as count from Task where email = $1"

func MakeTask( name *string, duration *float64, email *string)  *Task {
	task := new(Task)
	task.taskName = *name
	task.timeSpent = *duration
	task.email = *email
	return task
}

func AddTask( task *Task) error {
	_, stmterr := globalOrm.orm.Exec(insertTaskString, task.taskName, task.timeSpent, task.email);
	if stmterr != nil {
		log.Println("statement error : " + stmterr.Error())
	}
	return stmterr
}

func SelectTasksByEmail( email string ) ([]map[string]float64, error) {
	countRows, counterr := globalOrm.orm.Query(selectCountTasksByEmail, email)
	if (counterr != nil) {
		log.Println("statement error : " + counterr.Error())
		return nil, counterr
	}
	if countRows.Next() {
		
	} else {
		log.Println("Zero rows.")
		return nil, errors.New("No rows")
	}
	var rowsCount int
	countRows.Scan(&rowsCount)
	
	rows, stmterr := globalOrm.orm.Query(selectTasksByEmailString, email)
	if stmterr != nil {
		log.Println("statement error : " + stmterr.Error())
			return nil, stmterr
	} else {
			taskMap := make( []map[string]float64, rowsCount)
			taskMapCounter := 0
			var taskName string
			var timeSpent float64
			for rows.Next()  {
				rows.Scan( &taskName, &timeSpent)
				//log.Printf( "%s %.2f" ,taskName, timeSpent)
				taskMap[taskMapCounter] = make(map[string]float64)
				taskMap[taskMapCounter][taskName] = timeSpent
				taskMapCounter++
			}
			return taskMap[:], nil
		}
}