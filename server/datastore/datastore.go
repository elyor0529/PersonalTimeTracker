package datastore

import (
	"database/sql"
	_ "github.com/lib/pq"
	"log"
)

type datastore struct {
	orm *sql.DB
}

var globalOrm *datastore = nil

func SetUpOrm(databaseVendor *string,
	userName *string, password *string,
	hostName *string, hostPort *string,
	offlineDatabasePath *string, onlineDbName *string) bool {
	if globalOrm != nil {
		return true
	}
	var err error
	globalOrm = new(datastore)
	if *databaseVendor == "sqlite" {
		//globalOrm.orm, err = gorm.Open(*databaseVendor, *offlineDatabasePath)
	} else if *databaseVendor == "mysql" {
		var connectionString = *userName + ":" + *password + "@/" + *onlineDbName + "?charset=utf8&parseTime=True&loc=Local"
		log.Println(connectionString)
		//globalOrm.orm, err = gorm.Open(*databaseVendor, connectionString)
	} else if *databaseVendor == "postgres" {
		var connectionString = "host=" + *hostName + " port=" + *hostPort + " user=" + *userName + " dbname=" + *onlineDbName + " password=" + *password + " sslmode=disable"
		log.Println(connectionString)
		globalOrm.orm, err = sql.Open("postgres", connectionString)
	} else {
		log.Println("Unsupported connection string. Database setup Failed.")
		return false
	}

	if err != nil {
		log.Println("error while connecting to the database: " + err.Error())
		return false
	}

	_, stmterr := globalOrm.orm.Query("Select 2+1;")
	if stmterr != nil {
		return false
	}
	return true
}
