
import {sequelize} from "./data.js";
import { DataTypes, Sequelize } from "sequelize";



const entrada = sequelize.define('entrada',{

    idEntrada :{
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true
    },
    tipoEntrada: {
        type: DataTypes.ENUM('entrada turno','entrada almuerzo'),
        allowNull: false,
    },
    fechaEntrada :{
        type: DataTypes.DATEONLY,
        defaultValue: Sequelize.NOW,
        allowNull: false
    },
    horaEntrada: {
        type: DataTypes.TIME,
        allowNull: false
    },
    

}
,{
    timestamps: false,
    tableName: 'entrada'
  
})

entrada.associate = (models) =>{

    entrada.belongsTo(models.empleado,{
        foreignKey: {
            allowNull: false
        },
    })

    return entrada
}


export default entrada;