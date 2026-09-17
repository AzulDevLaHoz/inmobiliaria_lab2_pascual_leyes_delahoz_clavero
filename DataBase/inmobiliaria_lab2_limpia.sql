-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: inmobiliaria_lab2
-- ------------------------------------------------------
-- Server version	8.0.44

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `imagen`
--

DROP TABLE IF EXISTS `imagen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `imagen` (
  `idImagen` int NOT NULL AUTO_INCREMENT,
  `imagen` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `idInmueble` int NOT NULL,
  PRIMARY KEY (`idImagen`),
  KEY `FK_Imagen_Inmueble` (`idInmueble`),
  CONSTRAINT `FK_Imagen_Inmueble` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagen`
--

LOCK TABLES `imagen` WRITE;
/*!40000 ALTER TABLE `imagen` DISABLE KEYS */;
INSERT INTO `imagen` VALUES (1,'/Uploads/Galeria/b646d05e-d5b2-4715-a1ca-7709bf7beed3.webp',9),(3,'/Uploads/Galeria/8597cb6f-a532-42ee-829f-b058958ea5de.webp',9),(4,'/Uploads/Galeria/5d37eb99-8839-4e97-8a23-83c373260f98.webp',3),(5,'/Uploads/Galeria/bb434f5e-774b-46b0-8a12-ae1e3c8a125e.webp',3);
/*!40000 ALTER TABLE `imagen` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inmueble`
--

DROP TABLE IF EXISTS `inmueble`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inmueble` (
  `idInmueble` int NOT NULL AUTO_INCREMENT,
  `direccion` varchar(200) COLLATE utf8mb4_general_ci NOT NULL,
  `capacidad` int NOT NULL,
  `latitud` decimal(10,0) DEFAULT NULL,
  `longitud` decimal(10,0) DEFAULT NULL,
  `porcentajeReserva` decimal(5,2) NOT NULL,
  `imagenPortada` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `montoDia` decimal(10,0) NOT NULL,
  `estado` tinyint NOT NULL,
  `idPropietario` int NOT NULL,
  `idTipoInmueble` int NOT NULL,
  PRIMARY KEY (`idInmueble`),
  KEY `FK_Inmueble_Propietario` (`idPropietario`),
  KEY `FK_Inmueble_Tipo` (`idTipoInmueble`),
  CONSTRAINT `FK_Inmueble_Propietario` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`),
  CONSTRAINT `FK_Inmueble_Tipo` FOREIGN KEY (`idTipoInmueble`) REFERENCES `tipoinmueble` (`idTipoInmueble`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inmueble`
--

LOCK TABLES `inmueble` WRITE;
/*!40000 ALTER TABLE `inmueble` DISABLE KEYS */;
INSERT INTO `inmueble` VALUES (1,'siempreViva 123',5,45,33,50.00,'/Uploads/Portadas/7fc8de71-15d9-417c-968f-09dd26d34765.webp',50000,1,9,1),(3,'los Almendros 649',4,33,45,30.00,'/Uploads/Portadas/f982e3c2-0541-451e-bbe7-084525ac3b84.webp',50000,1,9,1),(9,'prueba 123',2,23,23,30.00,'/Uploads/Portadas/28705137-555a-4a23-b4f1-49f2c3cca40c.webp',60000,1,15,1);
/*!40000 ALTER TABLE `inmueble` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inquilino`
--

DROP TABLE IF EXISTS `inquilino`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inquilino` (
  `idInquilino` int NOT NULL AUTO_INCREMENT,
  `dni` varchar(20) COLLATE utf8mb4_general_ci NOT NULL,
  `nombre` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `apellido` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `telefono` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `email` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `estado` tinyint NOT NULL,
  PRIMARY KEY (`idInquilino`),
  UNIQUE KEY `dni` (`dni`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inquilino`
--

LOCK TABLES `inquilino` WRITE;
/*!40000 ALTER TABLE `inquilino` DISABLE KEYS */;
INSERT INTO `inquilino` VALUES (2,'123132312','Homero','Simpson','25533231','homero@simpson.com',0),(3,'123123','Lautaro','Martinez','76866666','torito22@gmail.com',0),(4,'12312333','Lautaro','Martinez','76866666','torito22@gmail.com',1),(5,'11222333','Homero','Simpson','2665252525','homero@simpson.com',1),(6,'21221221','martin','lopez','11223333','martin@fomd.com',1),(7,'32333212','harry','potter','2665333321','harry@potter.com',1),(8,'44323221','Lisa','simpson','11233233','lisa@simpson.com',1),(10,'12223456','roberto','carlos','547878777','rc@gmail.com',1),(11,'35123456','Juan Pablo','Gómez','2664123456','juan.gomez@gmail.com',1),(12,'38987654','María Laura','Fernández','2664987654','mlaura.fernandez@hotmail.com',1),(13,'32456789','Carlos Eduardo','Rodríguez','2664456789','carlos.rodriguez@yahoo.com',1),(14,'40112233','Sofia Belén','Martínez','2664112233','sofi.martinez@outlook.com',1),(15,'36778899','Lucas Matías','López','2664778899','lucas.lopez@gmail.com',1),(16,'41554433','Camila Agustina','Pérez','2664554433','camila.perez@live.com',1),(17,'33665544','Gonzalo Hernán','Sánchez','2664665544','gonzalo.sanchez@gmail.com',0);
/*!40000 ALTER TABLE `inquilino` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pago`
--

DROP TABLE IF EXISTS `pago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pago` (
  `idPago` int NOT NULL AUTO_INCREMENT,
  `concepto` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `importe` decimal(10,2) NOT NULL,
  `fechaPago` datetime NOT NULL,
  `metodoPago` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `estado` varchar(20) COLLATE utf8mb4_general_ci DEFAULT 'Vigente',
  `idReserva` int NOT NULL,
  `idUsuarioCreador` int NOT NULL,
  `idUsuarioAnulador` int DEFAULT NULL,
  PRIMARY KEY (`idPago`),
  KEY `FK_Pago_Reserva` (`idReserva`),
  KEY `FK_Pago_UsuCreador` (`idUsuarioCreador`),
  KEY `FK_Pago_UsuAnulador` (`idUsuarioAnulador`),
  CONSTRAINT `FK_Pago_Reserva` FOREIGN KEY (`idReserva`) REFERENCES `reserva` (`idReserva`),
  CONSTRAINT `FK_Pago_UsuAnulador` FOREIGN KEY (`idUsuarioAnulador`) REFERENCES `usuario` (`idUsuario`),
  CONSTRAINT `FK_Pago_UsuCreador` FOREIGN KEY (`idUsuarioCreador`) REFERENCES `usuario` (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pago`
--

LOCK TABLES `pago` WRITE;
/*!40000 ALTER TABLE `pago` DISABLE KEYS */;
/*!40000 ALTER TABLE `pago` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `propietario`
--

DROP TABLE IF EXISTS `propietario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `propietario` (
  `idPropietario` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(75) COLLATE utf8mb4_general_ci NOT NULL,
  `apellido` varchar(75) COLLATE utf8mb4_general_ci NOT NULL,
  `telefono` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `dni` varchar(25) COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `estado` tinyint NOT NULL,
  PRIMARY KEY (`idPropietario`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `propietario`
--

LOCK TABLES `propietario` WRITE;
/*!40000 ALTER TABLE `propietario` DISABLE KEYS */;
INSERT INTO `propietario` VALUES (2,'Leo ','Messi','222222222223','70077777','leomessi@his.com',0),(8,' Patricio Oscar','pascual','02665100116','33333333','patriciopascual2@gmail.com',0),(9,'Patricio Oscar','Pascual','2664302211','33333333','patriciopascual2@gmail.com',1),(15,'Adrian','Martinez','1152535433','34444333','adrian@maravilla.com',1),(16,'Roberto Daniel','Alvarez','2664111222','25111222','roberto.alvarez@gmail.com',1),(17,'Silvia Beatriz','Quiroga','2664333444','28333444','silvia.quiroga@hotmail.com',1),(18,'Marcelo Alejandro','Romero','2664555666','31555666','marcelo.romero@yahoo.com',1),(19,'Patricia Elizabeth','Torres','2664777888','24777888','patricia.torres@outlook.com',1),(20,'Jorge Alberto','Benítez','2664999000','29999000','jorge.benitez@gmail.com',1),(21,'Alicia Ester','Sosa','2664123987','27123987','alicia.sosa@live.com',1),(22,'Fernando Gabriel','Acosta','2664456654','30456654','fernando.acosta@gmail.com',1),(23,'Claudia Marcela','Medina','2664789987','26789987','claudia.medina@hotmail.com',1),(24,'Gustavo Adolfo','Castro','2664112244','33112244','gustavo.castro@yahoo.com',1),(25,'Marta Inés','Navarro','2664556677','28556677','marta.navarro@outlook.com',1);
/*!40000 ALTER TABLE `propietario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reserva`
--

DROP TABLE IF EXISTS `reserva`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reserva` (
  `idReserva` int NOT NULL AUTO_INCREMENT,
  `fechaEntrada` date NOT NULL,
  `fechaSalida` date NOT NULL,
  `estado` tinyint NOT NULL,
  `fechaMulta` date DEFAULT NULL,
  `multa` decimal(10,2) DEFAULT '0.00',
  `idInquilino` int NOT NULL,
  `idInmueble` int NOT NULL,
  PRIMARY KEY (`idReserva`),
  KEY `FK_Reserva_Inquilino` (`idInquilino`),
  KEY `FK_Reserva_Inmueble` (`idInmueble`),
  CONSTRAINT `FK_Reserva_Inmueble` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`),
  CONSTRAINT `FK_Reserva_Inquilino` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reserva`
--

LOCK TABLES `reserva` WRITE;
/*!40000 ALTER TABLE `reserva` DISABLE KEYS */;
/*!40000 ALTER TABLE `reserva` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rol`
--

DROP TABLE IF EXISTS `rol`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rol` (
  `idRol` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`idRol`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rol`
--

LOCK TABLES `rol` WRITE;
/*!40000 ALTER TABLE `rol` DISABLE KEYS */;
INSERT INTO `rol` VALUES (1,'Administrador'),(2,'Empleado');
/*!40000 ALTER TABLE `rol` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipoinmueble`
--

DROP TABLE IF EXISTS `tipoinmueble`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipoinmueble` (
  `idTipoInmueble` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`idTipoInmueble`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipoinmueble`
--

LOCK TABLES `tipoinmueble` WRITE;
/*!40000 ALTER TABLE `tipoinmueble` DISABLE KEYS */;
INSERT INTO `tipoinmueble` VALUES (1,'Casa'),(2,'Departamento'),(3,'quinta'),(4,'Local Comercial');
/*!40000 ALTER TABLE `tipoinmueble` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `idUsuario` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `apellido` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `clave` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `estado` tinyint(1) DEFAULT '1',
  `avatar` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `idRol` int NOT NULL,
  PRIMARY KEY (`idUsuario`),
  UNIQUE KEY `email` (`email`),
  KEY `FK_Usuario_Rol` (`idRol`),
  CONSTRAINT `FK_Usuario_Rol` FOREIGN KEY (`idRol`) REFERENCES `rol` (`idRol`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (5,'Patricio ','Pascual','patriciopascual2@gmail.com','AQAAAAIAAYagAAAAEEH9EhlXuSkTvHQiVUTPOAXQ92hdE3Eh6L0VbaFZHT+XkromHaUQJMOxD6jCfHNRrg==',1,NULL,1),(6,'Leandro','Leyes','leandro@gmail.com','AQAAAAIAAYagAAAAEMnulLTVBEVbVvh0XgeOMrSYW0NujjrGXn6HPAA3pZgnz4cx69c11pM8hD/EulieIw==',1,NULL,1),(7,'Azul','De La Hoz','Azul@gmail.com','AQAAAAIAAYagAAAAEOOe+82QWIV1+jaQFmIMkumV6AbEzMe6nxJy0+v7J7n2EYipSKTAn3TeIATwdZ5M2g==',1,NULL,2);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'inmobiliaria_lab2'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-09-17 17:26:07
