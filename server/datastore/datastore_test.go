package datastore

import (
//"net/http"
// "net/http/httptest"
  "os"
  "testing"
	"fmt"
	"log"
)

var database = os.Getenv("acs_database")
var databaseUserName = os.Getenv("acs_dbuser")
var databasePassword = os.Getenv("acs_dbpass")
var offlineDatabaseFile = os.Getenv("acs_offlineDbFile")
var onlineDatabaseName = os.Getenv("acs_onlineDbName")
var onlineDatabaseNameTest = os.Getenv("acs_onlineDbNameTest")
var hostName = os.Getenv("acs_hostname")
var hostPort = os.Getenv("acs_hostport")
	
func TestNewAccount(t *testing.T) {
	var email = "abc@abc.com"
	var timezone = 4
	var pass = "abc"
	var firstName = "Jeff"
	var lastName = "Greenwood"
	var middleName = "M."
	account := MakeAccount( &email, timezone, &pass, &firstName, &middleName, &lastName);
	addResult := AddAccount(account)
	if addResult != nil {
		log.Println("addResult failed. " + addResult.Error() )
		t.Fail()
	}
	existsResult := Exists(account)
	if  existsResult != nil {
		log.Println("Exists failed. " + existsResult.Error())
		t.Fail()
	}
}

func TestMatchEmailPassword (t *testing.T) {
	var account Account
	account.email = "abc@abc.com"
	account.encryptedPasswordHash = "abc"
	err := MatchEmailPassword(&account)
	if  err != nil {
		log.Println("MatchEmailPassword failed. " + err.Error())
		t.Fail()
	}
	
}

func TestMain(m *testing.M) {
	if database=="postgres" && 
    (databaseUserName == "" || 
      databasePassword == ""  || 
      onlineDatabaseName == "" || 
      hostName == "" || 
      hostPort == "") {
      fmt.Println("datastore_test is skipped due to insufficient environment variables.")
			os.Exit(1)
  } else if database == ""{
      fmt.Println("unsupported database vendor.")
			os.Exit(1)
  } else if database != "postgres" {
    fmt.Println("insufficient environment variables to run datastore_test. Please set acs_database, acs_dbuser, acs_dbpass, acs_offlineDbFile, acs_onlineDbName, acs_hostname, acs_hostport to the correct values");
		fmt.Printf("database is %s\n", hostName)
		os.Exit(1)
  }
	result := SetUpOrm( &database, &databaseUserName, &databasePassword, &hostName, &hostPort, &offlineDatabaseFile, &onlineDatabaseNameTest)
    if result == false {
      fmt.Println("failed to connect to database.")
			os.Exit(1)
    }
	defer globalOrm.orm.Close()
	os.Exit(m.Run())
}
