package main

import (
	"net/http"
	"acs560_course_project/server/businesslogic"
  "acs560_course_project/server/datastore"
  "flag"
  "log"
)

func main() {
  http.HandleFunc("/", businesslogic.Hey)
	http.HandleFunc("/newUnauthorizedSession", businesslogic.NewUnauthorizedSessionHandler)
	http.HandleFunc("/newAuthorizedSession", businesslogic.NewAuthorizedSessionHandler)
	http.HandleFunc("/new_user", businesslogic.NewUserHandler)
  http.HandleFunc("/Login", businesslogic.LogUserIn)
  http.HandleFunc("/NewUser", businesslogic.NewUserHandler)
  http.HandleFunc("/CreateAccount", businesslogic.CreateAccountHandler)
  
	http.ListenAndServe(":8000", nil);
  
}

func parseCmdFlagsSetUpOrm(){
  var database = flag.String("database", "sqlite", "the database to use (currently supported: sqlite, mysql)")
  var databaseUserName = flag.String("dbuser", "none", "database's user")
  var databasePassword = flag.String("dbpassword", "password", "database's password")
  var offlineDatabaseFile = flag.String("sqliteDbPath", ".\\data.db", "sqlite database file's path")
  var onlineDatabaseName = flag.String("dbname", "acs560", "database name in the online datastore")
  var hostName = flag.String("ipaddr", "127.0.0.1", "database server name")
  var hostPort = flag.String("port", "7000", "database server port")
	
  flag.Parse()
  
  var result = datastore.SetUpOrm(database, hostName, hostPort, databaseUserName, databasePassword, offlineDatabaseFile, onlineDatabaseName)
  log.Println("Setting up Orm...")
  if result == false {
    panic("Error while connecting to the database. Program exits.")
  } else {
    log.Println("Database: online.")
  }
}
