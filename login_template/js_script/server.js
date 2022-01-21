const express = require('express');
const app = express();
const bodyParser = require('body-parser');

// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: false }))

// Setting up DB
const mongoose = require('mongoose');
mongoose.connect("mongodb://localhost:27017/login_template", {
    useNewUrlParser: true,
    useUnifiedTopology: true
});

const db = mongoose.connection;
db.on('error', console.error.bind(console, 'connection error:'));
db.once('open', () => {
    console.log('Database connected');
});

// Setup database models
require('./model/Account');

// Setup the routes
require('./routes/authenticationRoutes')(app);

app.listen(3000, () => {
    console.log("Listening on 3000");
});
