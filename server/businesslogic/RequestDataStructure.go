package businesslogic

import (
	"acs560_course_project/server/datastore"
)

type LoginRequestData struct {
	Email      string
	Password   string
	TimeZone   int
	SessionKey string
}

type LoginRequestResult struct {
	LoginResult string
	SessionKey  string
}

type CreateAccountRequestData struct {
	FirstName  string
	MiddleName string
	LastName   string
	Email      string
	Password   string
	TimeZone   int
	SessionKey string
}

type CreateAccountRequestResult struct {
	CreateAccountResult string
	SessionKey          string
}

type UpdatePasswordRequest struct {
	Email string
	SessionKey string
}

type UpdatePasswordRequestResult struct {
	UpdatePasswordResult string
	SessionKey string

}

type AddTaskRequestData struct {
	TaskName   string
	TimeSpent  float64
	TaskDate   string
	SessionKey string
}

type AddTaskRequestResult struct {
	AddResult string
}

type AddSharedTaskRequest struct {
	TaskName string
	TaskDate string
	TaskTimeSpent float64
	EmailTo string
	SessionKey string 
}

type AddSharedTaskResult struct {
	AddSharedTaskResult string
	SessionKey string 
}

type GetAllSharedTasksByEmailToRequest struct {
	SessionKey string
}

type GetAllSharedTasksByEmailToResult struct {
	GetAllSharedTasksResult string
	SharedTaskList []datastore.SharedTaskFromDb
}

type RetrieveTaskListRequestData struct {
	SessionKey string
}

type RetrieveTaskListRequestResult struct {
	RetrieveTaskListResult string
	TaskList               []datastore.TaskFromDb
}
