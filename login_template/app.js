const express = require("express");
const keys = require("./config/keys");
const app = express();

//Setup DB
mongoose.connect(keys.mongoURI, {
    useNewUrlParser: true,
    useUnifiedTopology: true
});

//Setup db Models
require("./models/Account");
//Setup the Routes
require("./routes/authenticationRoutes")(app);
app.listen(3000, () => {
    console.log("Listening on " + keys.port);
})