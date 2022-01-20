const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const EnemySchema = new Schema({
    id:String,
    name:String,
    health:Number,
    attack:Number
});

module.exports = mongoose.model("Enemy",EnemySchema);