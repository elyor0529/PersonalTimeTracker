package datastore

import (
  "net/http"
  "net/http/httptest"
  "os"
  "testing"
)

var database = os.Getenv("acs_database")
var databaseUserName = os.Getenv("acs_dbuser")
var databasePassword = os.Getenv("acs_dbpass")
var offlineDatabaseFile = os.Getenv("acs_offlineDbFile")
var onlineDatabaseName = os.Getenv("acs_onlineDbName")
var hostName = os.Getenv("acs_hostname")
var hostPort = os.Getenv("acs_hostport")
	
func TestDatabaseConnection(t *testing.T) {
  if database=="postgres" && 
    (databaseUserName == "" || 
      databasePassword == ""  || 
      onlineDatabaseName == "" || 
      hostName == "" || 
      hostPort == "") {
      t.Skipf("datastore_test is skipped due to insufficient environment variables.")
  } else if database == ""{
      t.Skipf("unsupported database vendor.")
  } else {
    t.Skipf("insufficient environment variables to run datastore_test. Please set acs_database, acs_dbuser, acs_dbpass, acs_offlineDbFile, acs_onlineDbName, acs_hostname, acs_hostport to the correct values");
  }
  server := httptest.NewServer( http.HandlerFunc ( func ( writer http.ResponseWriter, requestPtr *http.Request ) {
    result := SetUpOrm( &database, &databaseUserName, &databasePassword, &hostName, &hostPort, &offlineDatabaseFile, &onlineDatabaseName)
    if result == false {
      t.Fail()
    }
  }))
  defer server.Close()
  http.Get(server.URL)
}