const express = require ("express");
const id = require('shortid');
const app = express();
// const mongoose = require('mongoose');
//const Enemy = require('./models/enemy');

// mongoose.connect("mongodb://localhost:27017/enemy_database",{
//     useNewUrlParser:true,
//     useUnifiedTopology:true
// });

// const db = mongoose.connection;
// db.on('error',console.error.bind(console,'connection error'));
// db.once('open',()=>{
//     console.log("Mongo connected");
// })

app.use(express.json());

var enemies = [
    {
        id:id.generate(),
        name:"orc",
        health:100,
        attack:25
    },
    {
        id:id.generate(),
        name :"wolf",
        health:110,
        attack:25
    }
];

app.get('/',(req,res)=>{
    res.send("hello unity Biatch");
})

app.post('/enemy/create',(req,res)=>{
    var newEnemy = {
        id:id.generate(),
        name: req.body.name,
        health:req.body.health,
        attack:req.body.attack
    };
    enemies.push(newEnemy);
    console.log(enemies);
    res.send(enemies);
})

app.get('/enemy',(req,res)=>{
    res.send(enemies);
})

// app.get('/enemy/orc',(req,res)=>{
//     res.send(enemies.orc);
// })

app.listen(3000, function(){
    console.log("listening on port 3000");
});