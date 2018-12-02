package businesslogic

import (
	"acs560_course_project/server/datastore"
)

func AddTask( request *AddTaskRequestData, sessionEmail string) AddTaskRequestResult{
	task := datastore.MakeTask( &(request.TaskName), &(request.TimeSpent), &sessionEmail, &(request.TaskDate))
	err := datastore.AddTask(task)
	if err != nil {
		return AddTaskRequestResult{
			AddResult : "Failure",
		}
	} else {
			return AddTaskRequestResult{
			AddResult : "Success",
		}
	}
}

func GetTasks( sessionEmail string ) RetrieveTaskListRequestResult{
	result,err := datastore.SelectTasksByEmail( sessionEmail )
	if err != nil {
		return RetrieveTaskListRequestResult {
			RetrieveTaskListResult: "Failure",
			TaskList: nil,
		}
	} else {
		return RetrieveTaskListRequestResult {
			RetrieveTaskListResult: "Success",
			TaskList: result[:],
		}
	}
}

func GetUniqueTaskNames ( sessionEmail string ) GetTaskNameSuggestionResult {
	result,err := datastore.GetUniqueTaskNames( sessionEmail )
	if err != nil {
		return GetTaskNameSuggestionResult {
			GetTaskNameSuggestionResult: "Failure",
			TaskNames: nil,
		}
	} else {
		return GetTaskNameSuggestionResult {
			GetTaskNameSuggestionResult: "Success",
			TaskNames: result[:],
		}
	}
}