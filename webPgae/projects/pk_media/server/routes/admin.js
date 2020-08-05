var express = require('express');
var router = express.Router();
var app = express();

const usersRouter = require("./users");
const posterInfoRouter = require("./posterInfo");

router.use("/users", usersRouter);
router.use("/posterInfo", posterInfoRouter);

router.get('/', function(req, res, next) {
    res.send('admin');
});

module.exports = router;