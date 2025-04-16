

import {sequelize} from "./data.js";
import { DataTypes, Sequelize } from "sequelize";


const salida = sequelize.define('salida',{

    idSalida :{
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true
    },
    tipoSalida: {
        type: DataTypes.ENUM('salida almuerzo', 'salida turno'),
        allowNull: false,
    },
    fechaSalida :{
        type: DataTypes.DATEONLY,
        defaultValue: Sequelize.NOW,
        allowNull: false
    },
    horaSalida: {
        type: DataTypes.TIME,
        allowNull: false
    },
    


}
,{
    timestamps: false,
    tableName: 'salida'
    
})

salida.associate = (models)=>{
    salida.belongsTo(models.empleado,{
        foreignKey: {
            allowNull: false,
        },
    })
    return salida
}


export default salida