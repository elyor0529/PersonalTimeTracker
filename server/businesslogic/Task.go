package businesslogic

import (
	"log"
)

func AddTask( request *AddTaskRequestData, sessionEmail string) AddTaskRequestResult{
	var space string = ""
	task := datastore.MakeTask( &(request.TaskName), &(request.TimeSpent), sessionEmail)
	err := datastore.AddTask(task)
	if err != nil {
		return &AddTaskRequestResult{
			AddResult : "Failure",
		}
	} else {
			return &AddTaskRequestResult{
			AddResult : "Success",
		}
	}
}

func GetTasks( body[] byte, sessionEmail string ) {
	result,err := SelectTasksByEmail( sessionEmail )
	if err != nil {
		return &RetrieveTaskListRequestResult {
			RetrieveTaskListResult: "Failure",
			TaskList: nil,
		}
	} else {
		return &RetrieveTaskListRequestResult {
			RetrieveTaskListResult: "Success",
			TaskList: result[:],
		}
	}
}