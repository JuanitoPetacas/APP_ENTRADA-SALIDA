import cors from 'cors'
import express from 'express'
import fs from 'fs'
import path from 'path'
import dotenv from 'dotenv'
import sequelize,{models} from './src/modules/associations.js'
import Empleado from './src/routes/empleado.js'
import Entrada from './src/routes/entrada.js'
import Salida from './src/routes/salida.js'
import Usuario from './src/routes/usuarios.js'




const corsOptions = {
    origin: '*', // Especifico la direccion de origen de la peticion
    methods: ['GET', 'POST', 'PUT', 'DELETE'], // Indico que peticiones http se van usar
    allowedHeaders: ['Content-Type', 'Authorization'], // Autoriza a los headers
    exposedHeaders: ['Access-Control-Allow-Origin']
};

const app = express();
app.use(cors(corsOptions))
app.use(express.json());
app.use(express.urlencoded({ extended: true }));
dotenv.config()


const port = process.env.PORT || 3000;


app.use(Entrada)
app.use(Salida)
app.use(Usuario)
app.use(Empleado)





app.listen(port,'0.0.0.0', ()=>{
    console.log(`Server is running on port ${port}`)
})

sequelize
.sync({force:false})
.then(()=>{
    console.log('base de datos sincronizada')
})
.catch((error)=>{
    console.log(`error en la sincronizacion: ${error}`)
})