package reception

var sessionManager map[string]string = make(map[string]string)

func authorizeSession( SessionKey string, Email string ) {
	sessionManager[SessionKey] = Email
}

//TODO: remove expired session from session manager