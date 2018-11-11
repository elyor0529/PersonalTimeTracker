package businesslogic 
import (
	"encoding/json"
  "fmt"
	"io"
	"io/ioutil"
	"log"
	"net/http"
)

func Hey ( writer http.ResponseWriter, requestPtr *http.Request ) {
	io.WriteString ( writer, "Test" )
}

func NewUnauthorizedSessionHandler ( writer http.ResponseWriter, requestPtr *http.Request ) {	
	sessionPtr := getNewUnauthorizedSession()
	jsonByteArr, error := json.Marshal( sessionPtr )

	if ( error != nil ) {
		log.Printf( "%s\n", error.Error() )
	}
	writer.Write ( jsonByteArr )
}

func NewAuthorizedSessionHandler ( writer http.ResponseWriter, requestPtr *http.Request ) {
  var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
	var incomingSession IncomingSession
	error = json.Unmarshal( body, incomingSession )
	if ( error != nil ) {
		log.Printf( "error %s\n", error.Error() );
	}
}

func NewUserHandler ( writer http.ResponseWriter, requestPtr *http.Request ) {
		io.WriteString ( writer, "Test" )
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
  
  loginResultPtr := new(LoginResult)
  loginResultPtr.LoginResult = "Success"
  jsonByteArr, error := json.Marshal( loginResultPtr )
  writer.Write ( jsonByteArr )
} 

func CreateAccountHandler ( writer http.ResponseWriter, requestPtr *http.Request ) {
  var bodyIncompleteReader io.ReadCloser = requestPtr.Body	
	body, error := ioutil.ReadAll( bodyIncompleteReader )
  fmt.Printf("len is %d\n",len(body))
  if  error != nil  {
		log.Printf( "error %s\n", error.Error() );
	}
  createAccountResultPtr := new(CreateAccountResult)
  createAccountResultPtr.CreateAccountResult = "Success"
  jsonByteArr, error := json.Marshal( createAccountResultPtr )
  writer.Write ( jsonByteArr )
}