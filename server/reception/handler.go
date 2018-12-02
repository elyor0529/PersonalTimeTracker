package reception 

import (
	"encoding/json"
	"fmt"
	"io"
	"io/ioutil"
	"log"
	"net/http"
	"acs560_course_project/server/businesslogic"
)

func Hey(writer http.ResponseWriter, requestPtr *http.Request) {
	io.WriteString(writer, "Test")
}

func NewUnauthorizedSessionHandler(writer http.ResponseWriter, requestPtr *http.Request) {
	sessionPtr := getNewUnauthorizedSession()
	jsonByteArr, error := json.Marshal(sessionPtr)

	if error != nil {
		log.Printf("%s\n", error.Error())
	}
	writer.Write(jsonByteArr)
}

func NewAuthorizedSessionHandler(writer http.ResponseWriter, requestPtr *http.Request) {
	var bodyIncompleteReader io.ReadCloser = requestPtr.Body
	body, error := ioutil.ReadAll(bodyIncompleteReader)
	var incomingSession IncomingSession
	error = json.Unmarshal(body, incomingSession)
	if error != nil {
		log.Printf("error %s\n", error.Error())
	}
}

func LogUserIn ( writer http.ResponseWriter, requestPtr *http.Request ) {
  fmt.Println("Log User In")
  
  fmt.Println(requestPtr.Body)
  fmt.Println()
  var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
  fmt.Printf("len is %d\n",len(body))
  if  error != nil  {
		log.Printf( "error %s\n", error.Error() );
	} 
  
  loginResultPtr, err, email := businesslogic.LogUserIn(body)
	if err == nil {
		sessionKey := string(generateUniqueSessionId())
		authorizeSession(sessionKey, email)
		loginResultPtr.SessionKey = sessionKey
	}
  jsonByteArr, error := json.Marshal( loginResultPtr )
  writer.Write ( jsonByteArr )
} 


func CreateAccountHandler(writer http.ResponseWriter, requestPtr *http.Request) {
	var bodyIncompleteReader io.ReadCloser = requestPtr.Body
	body, error := ioutil.ReadAll(bodyIncompleteReader)
	fmt.Printf("len is %d\n", len(body))
	if error != nil {
		log.Printf("error %s\n", error.Error())
	}

	responseData, err,email := businesslogic.CreateAccount(body)
  if err == nil {
		sessionKey := string(generateUniqueSessionId())
		authorizeSession(sessionKey, email)
		responseData.SessionKey = sessionKey
	}
	jsonByteArr, error := json.Marshal( responseData )
	writer.Write ( jsonByteArr )
}

func AddTaskHandler( writer http.ResponseWriter, requestPtr *http.Request ) {
	var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
  fmt.Printf("len is %d\n",len(body))
  if  error != nil  {
		log.Printf( "error %s\n", error.Error() );
	}
	var request businesslogic.AddTaskRequestData
	err := json.Unmarshal( body[:],  &request)
  var result businesslogic.AddTaskRequestResult
	if err != nil {
    log.Println(err.Error())
		result = businesslogic.AddTaskRequestResult {
			AddResult: "Incompatible JSON request structure.",
		}
  } else {
		result = businesslogic.AddTask(&request, getEmailBySession(request.SessionKey))
	}
	jsonByteArr, error := json.Marshal( result )
	writer.Write ( jsonByteArr )
}

func GetAllTasksHandler ( writer http.ResponseWriter, requestPtr *http.Request ) {
	var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
  fmt.Printf("len is %d\n",len(body))
  if  error != nil  {
		log.Printf( "error %s\n", error.Error() );
	}
	var request businesslogic.RetrieveTaskListRequestData
	err := json.Unmarshal( body[:],  &request)
  var result businesslogic.RetrieveTaskListRequestResult
	if err != nil {
    log.Println(err.Error())
		result = businesslogic.RetrieveTaskListRequestResult {
			RetrieveTaskListResult: "Incompatible JSON request structure.",
			TaskList: nil,
		}
  }
	result = businesslogic.GetTasks(getEmailBySession(request.SessionKey))
	jsonByteArr, error := json.Marshal( result )
	writer.Write ( jsonByteArr )
}

func RecoverPasswordHandler ( writer http.ResponseWriter, requestPtr *http.Request ) {
	var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
  fmt.Printf("len is %d\n",len(body))
  if  error != nil  {
		log.Printf( "error %s\n", error.Error() );
	}
	responseData, err  := businesslogic.RecoverPassword(body)
	if err == nil {
		writer.WriteHeader( http.StatusOK)
	} else {
		writer.WriteHeader( http.StatusInternalServerError)
	}
	jsonByteArr, _ := json.Marshal( responseData )
	writer.Write ( jsonByteArr )
}

func ShareTaskWithHandler( writer http.ResponseWriter, requestPtr *http.Request ) {
	var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
  fmt.Printf("len is %d\n",len(body))
  if  error != nil  {
		log.Printf( "error %s\n", error.Error() );
	}
	var request businesslogic.AddSharedTaskRequest
	err := json.Unmarshal( body[:],  &request)
	var result businesslogic.AddSharedTaskResult

	if err != nil {
    log.Println(err.Error())
		result = businesslogic.AddSharedTaskResult {
			AddSharedTaskResult: "Incompatible JSON request structure.",
			SessionKey: request.SessionKey}
  } else {
		result = businesslogic.AddSharedTask(&request, getEmailBySession(request.SessionKey))
	}
	jsonByteArr, error := json.Marshal( result )
	writer.Write ( jsonByteArr )
}

func GetAllTasksSharedWithMeHandler(writer http.ResponseWriter, requestPtr *http.Request ) {
	var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
  fmt.Printf("len is %d\n",len(body))
  if  error != nil  {
		log.Printf( "error %s\n", error.Error() );
	}
	var request businesslogic.GetAllSharedTasksByEmailToRequest
	err := json.Unmarshal( body[:],  &request)
  var result businesslogic.GetAllSharedTasksByEmailToResult
	if err != nil {
    log.Println(err.Error())
		result = businesslogic.GetAllSharedTasksByEmailToResult {
			GetAllSharedTasksResult: "Incompatible JSON request structure.",
			SharedTaskList: nil,
		}
  }
	result = businesslogic.GetAllTasksSharedWithMe(getEmailBySession(request.SessionKey))
	jsonByteArr, error := json.Marshal( result )
	writer.Write ( jsonByteArr )
}

func GetTaskNameSuggestionHandler (writer http.ResponseWriter, requestPtr *http.Request ) {
	var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
  fmt.Printf("len is %d\n",len(body))
  if  error != nil  {
		log.Printf( "error %s\n", error.Error() );
	}
	var request businesslogic.GetTaskNameSuggestionRequest
	err := json.Unmarshal( body[:],  &request)
  var result businesslogic.GetTaskNameSuggestionResult
	if err != nil {
    log.Println(err.Error())
		result = businesslogic.GetTaskNameSuggestionResult {
			GetTaskNameSuggestionResult: "Incompatible JSON request structure.",
			TaskNames: nil,
		}
  } else {
		result = businesslogic.GetUniqueTaskNames(getEmailBySession(request.SessionKey))
	}
	jsonByteArr, error := json.Marshal( result )
	writer.Write ( jsonByteArr )
}