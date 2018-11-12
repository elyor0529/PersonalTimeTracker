package businesslogic

type LoginRequestData struct {
  Email string
	Password string
	TimeZone int
  SessionKey string
}

type LoginRequestResult struct{
  LoginResult string
  SessionKey string
}

type CreateAccountRequestData struct {
  FirstName string
  MiddleName string
  LastName string
  Email string
  Password string
	TimeZone int
  SessionKey string
}

type CreateAccountRequestResult struct {
  CreateAccountResult string
  SessionKey string
}