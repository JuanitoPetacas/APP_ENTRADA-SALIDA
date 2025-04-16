import { Sequelize } from 'sequelize';
import dotenv from 'dotenv';

dotenv.config();

console.log("Conectando a MySQL con:");
console.log("DB_HOST:", process.env.DB_HOST);
console.log("DB_USER:", process.env.DB_USER);
console.log("DB_PASSWORD:", process.env.DB_PASSWORD);
console.log("DB_NAME:", process.env.DB_NAME);
console.log("DB_PORT:", process.env.DB_PORT);

export const sequelize = new Sequelize(
  process.env.DB_NAME,
  process.env.DB_USER,
  process.env.DB_PASSWORD,
  {
    host: process.env.DB_HOST,
    dialect: 'mysql',
    port: process.env.DB_PORT || 3306,  // Puerto interno del contenedor MySQL
    timezone: '-05:00',  // Zona horaria para Colombia (UTC-5)
    logging: console.log,  // Imprime las consultas SQL en consola
  }
);

sequelize.authenticate()
  .then(() => console.log('✅ DB connected'))
  .catch((error) => {
    console.log('❌ DB connection failed:', error);
  });