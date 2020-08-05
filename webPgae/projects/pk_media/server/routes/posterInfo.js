var express = require('express')
var router = express.Router()
var db = require('../database/connect')

router.post('/search', function (req, res, next) {
    var sql = 'SELECT * FROM posterinfo'
    var params = req.body
    console.log('params:', sql)
    db.query(sql,[],function (error, result, fields) {
        if (error) {
            console.log('err:', error)
        }
        if (result) {
            console.log('result:', result)
            res.send(result)
        }
    })
})

module.exports = router