package businesslogic

import (
	"acs560_course_project/server/datastore"
	"encoding/json"
	"errors"
	"log"
	"net/smtp"
	"os"
	"math/rand"
	"time"
	"strconv"
)

func LogUserIn(body []byte) (*LoginRequestResult, error, string) {
	var request LoginRequestData
	err := json.Unmarshal(body[:], &request)
	if err != nil {
		log.Println(err.Error())
		return &LoginRequestResult{
			LoginResult: "Incompatible JSON request structure.",
			SessionKey:  request.SessionKey,
		}, err, ""
	}
	var space string = ""
	account := datastore.MakeAccount(&request.Email, request.TimeZone, &request.Password, &space, &space, &space)
	valid := accountValid(account)
	if valid == true {
		return &LoginRequestResult{
			LoginResult: "Success",
			SessionKey:  "",
		}, nil, request.Email
	} else {
		return &LoginRequestResult{
			LoginResult: "Failure",
			SessionKey:  request.SessionKey,
		}, errors.New("Invalid email/password."), ""
	}
}

func CreateAccount(body []byte) (*CreateAccountRequestResult, error, string) {
	var request CreateAccountRequestData
	err := json.Unmarshal(body[:], &request)
	if err != nil {
		log.Println(err.Error())
		return &CreateAccountRequestResult{
			CreateAccountResult: "Incompatible JSON request structure.",
			SessionKey:          request.SessionKey,
		}, err, ""
	}

	account := datastore.MakeAccount(&request.Email, request.TimeZone, &request.Password, &request.FirstName, &request.MiddleName, &request.LastName)
	exists := accountExists(account)
	if exists == true {
		return &CreateAccountRequestResult{
			CreateAccountResult: "Account already exists.",
			SessionKey:          request.SessionKey,
		}, errors.New("Account already exists."), ""
	} else {
		datastore.AddAccount(account)
		return &CreateAccountRequestResult{
			CreateAccountResult: "Success",
			SessionKey:          "",
		}, nil, request.Email
	}
}


func RecoverPassword(body []byte) (*UpdatePasswordRequestResult, error){
	var request UpdatePasswordRequest
	err := json.Unmarshal(body[:], &request)
	if err != nil {
		log.Println(err.Error())
		return &UpdatePasswordRequestResult{
			UpdatePasswordResult: "Incompatible JSON request structure.",
			SessionKey:          request.SessionKey,
		}, err
	} else { 
			newPassword := generateRandomPassword()
			
			account := new (datastore.Account)
			datastore.SetEmail( account, request.Email )
			
			if accountExists(account) == false {
				return &UpdatePasswordRequestResult{
					UpdatePasswordResult: "This email account does not exist.",
					SessionKey:          request.SessionKey,
				}, errors.New("This email account does not exist.")
			}
			databaseRecoverAccountError := datastore.RecoverPassword(account, newPassword)
			if databaseRecoverAccountError != nil {
				return &UpdatePasswordRequestResult{
					UpdatePasswordResult: "Failure",
					SessionKey:          request.SessionKey,
				}, databaseRecoverAccountError
			}
			smtpError := sendUpdatePasswordEmail(request.Email, newPassword)
			if smtpError == nil {
				return &UpdatePasswordRequestResult{
					UpdatePasswordResult: "Success",
					SessionKey:          request.SessionKey,
				}, smtpError
			} else {
				return &UpdatePasswordRequestResult{
					UpdatePasswordResult: "Internal server error while sending new password to the email.",
					SessionKey:          request.SessionKey,
				}, smtpError
			}
	}
}


func sendUpdatePasswordEmail(recipientEmail string, newPassword string ) (error) {
	from := os.Getenv("acs_SendingEmail")
	pass := os.Getenv("acs_SendingEmailPassword")
	to := recipientEmail
	body := "Your new password is " + newPassword
	msg := "From: " + from + "\r\n" +
					"To: " + to + "\r\n" +
					"Subject: New Password " + newPassword + "\r\n\r\n" +
					body

	err := smtp.SendMail("smtp.gmail.com:587",
		smtp.PlainAuth("", from, pass, "smtp.gmail.com"),
		from, []string{to}, []byte(msg))

	if err != nil {
		log.Printf("smtp error: %s", err)
	}
	return err
}

func generateRandomPassword() (string){
	var maxNumber int = 10000
	rand.Seed(time.Now().UnixNano())
	return strconv.FormatInt(int64(rand.Intn(maxNumber)), 10)
}

func accountExists(account *datastore.Account) bool {
	result := datastore.Exists(account)
	if result == nil {
		return true
	} else {
		return false
	}
}

func accountValid(account *datastore.Account) bool {
	result := datastore.MatchEmailPassword(account)
	if result == nil {
		return true
	} else {
		return false
	}
}


