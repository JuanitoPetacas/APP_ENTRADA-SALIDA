
import exp from 'constants';
import {sequelize} from './data.js'
import empleado from "./empleado.js";
import entrada from "./entrada.js";
import salida from "./salida.js";
import usuario from "./usuario.js";

const models = {empleado, entrada, salida, usuario}


Object.keys(models).forEach(modelName => {
    if ("associate" in models[modelName]) {
        models[modelName].associate(models);
    }
})



export {models}
export default sequelize