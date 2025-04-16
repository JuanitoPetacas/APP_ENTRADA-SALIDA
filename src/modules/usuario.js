
import { sequelize } from "./data.js";
import { DataTypes } from "sequelize";


const usuario = sequelize.define('usuario',{

    idUsuario: {
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true
    },
    tipoUsuario:{
        type: DataTypes.ENUM('ADMINISTRADOR','ORIENTADOR'),
        allowNull: false

    },
    nombreUsuario: {
        type: DataTypes.STRING,
        allowNull: false
    },
    numeroDocumento: {
        type: DataTypes.BIGINT,
        allowNull: false

    },
    passwordUsuario: {
        type: DataTypes.STRING,
        allowNull: false
    },
    estado:{
        type: DataTypes.ENUM('ACTIVO', 'INACTIVO'),
        defaultValue: 'ACTIVO',
        allowNull: false
    },
  
    

}
,{
    timestamps: false,
    tableName: 'usuario'
   
})

export default usuario;