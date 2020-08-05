// connect.js
var models = require('./db')
var mysql = require('mysql')
var conn = mysql.createConnection(models.mysql)
conn.connect(function (err) {
    if (err) {
        console.log(err)
    } else {
        console.log('database connect success')
    }
})

module.exports = conn;