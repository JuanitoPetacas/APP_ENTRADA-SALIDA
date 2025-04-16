
import { sequelize } from "./data.js";
import { DataTypes } from "sequelize";



const empleado = sequelize.define('empleado', {

    idEmpleado: {
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true
    },
    tipoEmpleado: {
        type: DataTypes.STRING,
        allowNull: false
    },
    nombreEmpleado:{
        type: DataTypes.STRING,
        allowNull: false
    },
    apellidoEmpleado:{
        type: DataTypes.STRING,
        allowNull: false
    },
    tipoDocumento:{
        type: DataTypes.STRING,
        allowNull: false
    },
    numeroDocumento:{
        type: DataTypes.BIGINT,
        allowNull: false
    },
    cargo: {
        type: DataTypes.STRING,
        allowNull: false
    },
    RH: {
        type: DataTypes.STRING,
        allowNull: false
    },
    estado: {
        type: DataTypes.ENUM('ACTIVO', 'INACTIVO'),
        defaultValue: 'ACTIVO',
        allowNull: false
    },

    
},{
    timestamps: false,
    tableName: 'empleado'
    
})

empleado.associate =(models)=>{
    empleado.hasMany(models.entrada,{
        foreignKey:{
            allowNull:false,
        }
    })
    empleado.hasMany(models.salida,{
        foreignKey:{
            allowNull:false,
        }
    })
    return empleado
}

export default empleado;