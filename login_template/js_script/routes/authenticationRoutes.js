const mongoose = require('mongoose');
const Account = mongoose.model('accounts');

const bcrypt = require("bcrypt");
const passwordRegex = new RegExp("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,24})");

module.exports = app => {
    // Routes
    app.post('/account/login', async (req, res) => {

        var response = {};

        const { rUsername, rPassword } = req.body;
        if(rUsername == null || !passwordRegex.test(rPassword))
        {
            response.code = 1;
            response.msg = "Invalid credentials";
            res.send(response);
            return;
        }

        var userAccount = await Account.findOne({ username: rUsername}, 'username adminFlag password');
        if(userAccount != null){
            bcrypt.compare(userAccount.password, rPassword, async (success) => {
                if(success){
                    userAccount.lastAuthentication = Date.now();
                    await userAccount.save();

                    response.code = 0;
                    response.msg = "Account found";
                    response.data = ( ({username, adminFlag}) => ({ username, adminFlag }) )(userAccount);
                    res.send(response);

                    return;
                }
                else{
                    response.code = 1;
                    response.msg = "Invalid credentials";
                    res.send(response);
                    return;
                }
            });
        }
        else{
            response.code = 1;
            response.msg = "Invalid credentials";
            res.send(response);
            return;
        }
    });

    app.post('/account/create', async (req, res) => {

        var response = {};

        const { rUsername, rPassword } = req.body;
        if(rUsername == null || rUsername.length < 3 || rUsername.length > 24)
        {
            response.code = 1;
            response.msg = "Invalid credentials";
            res.send(response);
            return;
        }

        //console.log(passwordRegex);
        console.log(rPassword);
        if(!passwordRegex.test(rPassword))
        {
            response.code = 3;
            response.msg = "Unsafe password";
            res.send(response);
            return;
        }

        var userAccount = await Account.findOne({ username: rUsername},'_id');
        if(userAccount == null){
            // Create a new account
            console.log("Create new account...")

            // Generate a unique access token
            bcrypt.genSalt(10, function(err, salt) {
                if(err){
                    console.log(err);
                }

                bcrypt.hash(rPassword, salt,async(hash)=>{
                    var newAccount = new Account({
                        username : rUsername,
                        password : hash,
                        salt: salt,
        
                        lastAuthentication : Date.now()
                    });
                    await newAccount.save();
                    console.log(newAccount);
                    
                    response.code = 0;
                    response.msg = "Account found";
                    response.data = ( ({username}) => ({ username }) )(newAccount);
                    res.send(response);
                    return;
                });
            });
        } else {
            response.code = 2;
            response.msg = "Username is already taken";
            res.send(response);
        }
        
        return;
    });
}
