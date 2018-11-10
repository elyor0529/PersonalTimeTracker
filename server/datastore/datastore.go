package datastore
import (
    "github.com/jinzhu/gorm"
  _ "github.com/jinzhu/gorm/dialects/mysql"
//  _ "github.com/jinzhu/gorm/dialects/sqlite"
  _ "github.com/jinzhu/gorm/dialects/postgres"
  "log"
)

type datastore struct{
  orm *gorm.DB
}

var globalOrm *gorm.DB
func SetUpOrm(databaseVendor *string, 
                          userName *string, password *string, 
                          hostName *string, hostPort *string,
                          offlineDatabasePath *string, onlineDbName *string) bool {
  var err error
  globalOrm = nil
  if *databaseVendor == "sqlite"{
    globalOrm, err = gorm.Open(*databaseVendor, *offlineDatabasePath)
  } else if *databaseVendor == "mysql"{
    var connectionString = *userName + ":" + *password + "@/" + *onlineDbName + "?charset=utf8&parseTime=True&loc=Local"
    log.Println(connectionString)
    globalOrm, err = gorm.Open(*databaseVendor, connectionString)
  } else if *databaseVendor == "postgres" {
    var connectionString = "host=" + *hostName + "port=" + *hostPort + "user=" + *userName + "dbname=" + *onlineDbName + "password=" + *password
    log.Println(connectionString)
    globalOrm, err = gorm.Open(*databaseVendor, connectionString)
  }
  
  if err != nil {
    log.Println("error while connecting to the database: " + err.Error())
    return false
  }
  
  return true
}