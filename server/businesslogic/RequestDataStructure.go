package businesslogic

type userLoginRequest struct {
	UserName   string
	Email      string
	SessionKey string
}

type LoginResult struct {
	LoginResult string
	SessionKey  string
}

type CreateAccountRequest struct {
	FirstName  string
	MiddleName string
	LastName   string
	Email      string
	Password   string
	SessionKey string
}

type CreateAccountResult struct {
	CreateAccountResult string
	SessionKey          string
}
