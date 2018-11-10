package datastore

import (
  "test"
  "net/http"
  "net/http/httptest"
)

func TestDatabaseConnection(t *testing.T) {
  server := httptest.NewServer( func( writer http.ResponseWriter, requestPtr *http.Request ) {
    
  });
}