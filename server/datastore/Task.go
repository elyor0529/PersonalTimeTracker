package datastore
import (
  "github.com/jinzhu/gorm"
)
type Task struct {
  gorm.Model
  taskId int
  taskName string
  timeSpent float32
  userId int
}

