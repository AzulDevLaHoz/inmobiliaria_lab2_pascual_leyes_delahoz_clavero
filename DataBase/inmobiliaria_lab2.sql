-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 14-09-2026 a las 20:41:13
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria_lab2`
--
CREATE DATABASE IF NOT EXISTS `inmobiliaria_lab2` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `inmobiliaria_lab2`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `imagen`
--

CREATE TABLE `imagen` (
  `idImagen` int(11) NOT NULL,
  `imagen` varchar(255) NOT NULL,
  `idInmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `imagen`
--

INSERT INTO `imagen` (`idImagen`, `imagen`, `idInmueble`) VALUES
(1, '/Uploads/Galeria/b646d05e-d5b2-4715-a1ca-7709bf7beed3.webp', 9),
(3, '/Uploads/Galeria/8597cb6f-a532-42ee-829f-b058958ea5de.webp', 9),
(4, '/Uploads/Galeria/5d37eb99-8839-4e97-8a23-83c373260f98.webp', 3),
(5, '/Uploads/Galeria/bb434f5e-774b-46b0-8a12-ae1e3c8a125e.webp', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `direccion` varchar(200) NOT NULL,
  `capacidad` int(11) NOT NULL,
  `latitud` decimal(10,0) DEFAULT NULL,
  `longitud` decimal(10,0) DEFAULT NULL,
  `porcentajeReserva` decimal(5,2) NOT NULL,
  `imagenPortada` varchar(255) DEFAULT NULL,
  `montoDia` decimal(10,0) NOT NULL,
  `estado` tinyint(4) NOT NULL,
  `idPropietario` int(11) NOT NULL,
  `idTipoInmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `direccion`, `capacidad`, `latitud`, `longitud`, `porcentajeReserva`, `imagenPortada`, `montoDia`, `estado`, `idPropietario`, `idTipoInmueble`) VALUES
(1, 'siempreViva 123', 5, 45, 33, 50.00, '/Uploads/Portadas/7fc8de71-15d9-417c-968f-09dd26d34765.webp', 50000, 1, 9, 1),
(3, 'los Almendros 649', 4, 33, 45, 30.00, '/Uploads/Portadas/f982e3c2-0541-451e-bbe7-084525ac3b84.webp', 50000, 1, 9, 1),
(9, 'prueba 123', 2, 23, 23, 30.00, '/Uploads/Portadas/28705137-555a-4a23-b4f1-49f2c3cca40c.webp', 60000, 1, 15, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(11) NOT NULL,
  `dni` varchar(20) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `estado` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `dni`, `nombre`, `apellido`, `telefono`, `email`, `estado`) VALUES
(2, '123132312', 'Homero', 'Simpson', '25533231', 'homero@simpson.com', 0),
(3, '123123', 'Lautaro', 'Martinez', '76866666', 'torito22@gmail.com', 0),
(4, '12312333', 'Lautaro', 'Martinez', '76866666', 'torito22@gmail.com', 1),
(5, '11222333', 'Homero', 'Simpson', '2665252525', 'homero@simpson.com', 1),
(6, '21221221', 'martin', 'lopez', '11223333', 'martin@fomd.com', 1),
(7, '32333212', 'harry', 'potter', '2665333321', 'harry@potter.com', 1),
(8, '44323221', 'Lisa', 'simpson', '11233233', 'lisa@simpson.com', 1),
(10, '12223456', 'roberto', 'carlos', '547878777', 'rc@gmail.com', 1),
(11, '35123456', 'Juan Pablo', 'Gómez', '2664123456', 'juan.gomez@gmail.com', 1),
(12, '38987654', 'María Laura', 'Fernández', '2664987654', 'mlaura.fernandez@hotmail.com', 1),
(13, '32456789', 'Carlos Eduardo', 'Rodríguez', '2664456789', 'carlos.rodriguez@yahoo.com', 1),
(14, '40112233', 'Sofia Belén', 'Martínez', '2664112233', 'sofi.martinez@outlook.com', 1),
(15, '36778899', 'Lucas Matías', 'López', '2664778899', 'lucas.lopez@gmail.com', 1),
(16, '41554433', 'Camila Agustina', 'Pérez', '2664554433', 'camila.perez@live.com', 1),
(17, '33665544', 'Gonzalo Hernán', 'Sánchez', '2664665544', 'gonzalo.sanchez@gmail.com', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `idPago` int(11) NOT NULL,
  `concepto` varchar(100) NOT NULL,
  `importe` decimal(10,2) NOT NULL,
  `fechaPago` datetime NOT NULL,
  `metodoPago` varchar(50) DEFAULT NULL,
  `estado` varchar(20) DEFAULT 'Vigente',
  `idReserva` int(11) NOT NULL,
  `idUsuarioCreador` int(11) NOT NULL,
  `idUsuarioAnulador` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`idPago`, `concepto`, `importe`, `fechaPago`, `metodoPago`, `estado`, `idReserva`, `idUsuarioCreador`, `idUsuarioAnulador`) VALUES
(3, 'Completado', 100000.00, '2026-09-11 00:00:00', 'efectivo', '1', 9, 1, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(11) NOT NULL,
  `nombre` varchar(75) NOT NULL,
  `apellido` varchar(75) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `dni` varchar(25) NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `estado` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `nombre`, `apellido`, `telefono`, `dni`, `email`, `estado`) VALUES
(2, 'Leo ', 'Messi', '222222222223', '70077777', 'leomessi@his.com', 0),
(8, ' Patricio Oscar', 'pascual', '02665100116', '33333333', 'patriciopascual2@gmail.com', 0),
(9, 'Patricio Oscar', 'Pascual', '2664302211', '33333333', 'patriciopascual2@gmail.com', 1),
(15, 'Adrian', 'Martinez', '1152535433', '34444333', 'adrian@maravilla.com', 1),
(16, 'Roberto Daniel', 'Alvarez', '2664111222', '25111222', 'roberto.alvarez@gmail.com', 1),
(17, 'Silvia Beatriz', 'Quiroga', '2664333444', '28333444', 'silvia.quiroga@hotmail.com', 1),
(18, 'Marcelo Alejandro', 'Romero', '2664555666', '31555666', 'marcelo.romero@yahoo.com', 1),
(19, 'Patricia Elizabeth', 'Torres', '2664777888', '24777888', 'patricia.torres@outlook.com', 1),
(20, 'Jorge Alberto', 'Benítez', '2664999000', '29999000', 'jorge.benitez@gmail.com', 1),
(21, 'Alicia Ester', 'Sosa', '2664123987', '27123987', 'alicia.sosa@live.com', 1),
(22, 'Fernando Gabriel', 'Acosta', '2664456654', '30456654', 'fernando.acosta@gmail.com', 1),
(23, 'Claudia Marcela', 'Medina', '2664789987', '26789987', 'claudia.medina@hotmail.com', 1),
(24, 'Gustavo Adolfo', 'Castro', '2664112244', '33112244', 'gustavo.castro@yahoo.com', 1),
(25, 'Marta Inés', 'Navarro', '2664556677', '28556677', 'marta.navarro@outlook.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reserva`
--

CREATE TABLE `reserva` (
  `idReserva` int(11) NOT NULL,
  `fechaEntrada` date NOT NULL,
  `fechaSalida` date NOT NULL,
  `estado` tinyint(20) NOT NULL,
  `fechaMulta` date DEFAULT NULL,
  `multa` decimal(10,2) DEFAULT 0.00,
  `idInquilino` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `reserva`
--

INSERT INTO `reserva` (`idReserva`, `fechaEntrada`, `fechaSalida`, `estado`, `fechaMulta`, `multa`, `idInquilino`, `idInmueble`) VALUES
(7, '2026-09-22', '2026-09-25', 1, NULL, NULL, 5, 9),
(8, '2026-09-10', '2026-09-13', 1, NULL, NULL, 4, 3),
(9, '2026-09-10', '2026-09-12', 1, NULL, NULL, 12, 1),
(10, '2026-09-12', '2026-09-13', 1, NULL, NULL, 6, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rol`
--

CREATE TABLE `rol` (
  `idRol` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `rol`
--

INSERT INTO `rol` (`idRol`, `nombre`) VALUES
(1, 'Administrador');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoinmueble`
--

CREATE TABLE `tipoinmueble` (
  `idTipoInmueble` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipoinmueble`
--

INSERT INTO `tipoinmueble` (`idTipoInmueble`, `nombre`) VALUES
(1, 'Casa'),
(2, 'Departamento'),
(3, 'quinta'),
(4, 'Local Comercial');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `clave` varchar(255) NOT NULL,
  `estado` tinyint(1) DEFAULT 1,
  `avatar` varchar(255) DEFAULT NULL,
  `idRol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `nombre`, `apellido`, `email`, `clave`, `estado`, `avatar`, `idRol`) VALUES
(1, '', '', 'admin@inmo.com', '1234', 1, NULL, 1),
(5, 'patricio ', 'pascual', 'patriciopascual2@gmail.com', 'AQAAAAIAAYagAAAAEEH9EhlXuSkTvHQiVUTPOAXQ92hdE3Eh6L0VbaFZHT+XkromHaUQJMOxD6jCfHNRrg==', 1, NULL, 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `imagen`
--
ALTER TABLE `imagen`
  ADD PRIMARY KEY (`idImagen`),
  ADD KEY `FK_Imagen_Inmueble` (`idInmueble`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `FK_Inmueble_Propietario` (`idPropietario`),
  ADD KEY `FK_Inmueble_Tipo` (`idTipoInmueble`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`),
  ADD UNIQUE KEY `dni` (`dni`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `FK_Pago_Reserva` (`idReserva`),
  ADD KEY `FK_Pago_UsuCreador` (`idUsuarioCreador`),
  ADD KEY `FK_Pago_UsuAnulador` (`idUsuarioAnulador`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `reserva`
--
ALTER TABLE `reserva`
  ADD PRIMARY KEY (`idReserva`),
  ADD KEY `FK_Reserva_Inquilino` (`idInquilino`),
  ADD KEY `FK_Reserva_Inmueble` (`idInmueble`);

--
-- Indices de la tabla `rol`
--
ALTER TABLE `rol`
  ADD PRIMARY KEY (`idRol`);

--
-- Indices de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  ADD PRIMARY KEY (`idTipoInmueble`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `FK_Usuario_Rol` (`idRol`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `imagen`
--
ALTER TABLE `imagen`
  MODIFY `idImagen` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT de la tabla `reserva`
--
ALTER TABLE `reserva`
  MODIFY `idReserva` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `rol`
--
ALTER TABLE `rol`
  MODIFY `idRol` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  MODIFY `idTipoInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `imagen`
--
ALTER TABLE `imagen`
  ADD CONSTRAINT `FK_Imagen_Inmueble` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `FK_Inmueble_Propietario` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`),
  ADD CONSTRAINT `FK_Inmueble_Tipo` FOREIGN KEY (`idTipoInmueble`) REFERENCES `tipoinmueble` (`idTipoInmueble`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `FK_Pago_Reserva` FOREIGN KEY (`idReserva`) REFERENCES `reserva` (`idReserva`),
  ADD CONSTRAINT `FK_Pago_UsuAnulador` FOREIGN KEY (`idUsuarioAnulador`) REFERENCES `usuario` (`idUsuario`),
  ADD CONSTRAINT `FK_Pago_UsuCreador` FOREIGN KEY (`idUsuarioCreador`) REFERENCES `usuario` (`idUsuario`);

--
-- Filtros para la tabla `reserva`
--
ALTER TABLE `reserva`
  ADD CONSTRAINT `FK_Reserva_Inmueble` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`),
  ADD CONSTRAINT `FK_Reserva_Inquilino` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`);

--
-- Filtros para la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `FK_Usuario_Rol` FOREIGN KEY (`idRol`) REFERENCES `rol` (`idRol`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
