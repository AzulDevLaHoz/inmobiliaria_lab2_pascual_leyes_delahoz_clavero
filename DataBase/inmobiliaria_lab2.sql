-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 03-09-2026 a las 20:34:19
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
(1, 'siempreViva 123', 5, 45, 33, 50.00, 'Microsoft.AspNetCore.Http.FormFile', 45000, 1, 9, 1),
(3, 'los Almendros 649', 4, 33, 45, 30.00, 'Microsoft.AspNetCore.Http.FormFile', 50000, 1, 9, 1),
(4, 'Lavalle 3122', 2, 22, 12, 20.00, NULL, 43000, 1, 9, 2),
(8, 'prueba 123', 2, 23, 23, 30.00, '/Uploads/Portadas/b8b03ce0-0faa-4d68-a376-3c6b0d176636.webp', 30000, 1, 9, 1);

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
(4, '12312332', 'Lautaro', 'Martinez', '76866666', 'torito22@gmail.com', 1),
(5, '11222333', 'Homero', 'Simpson', '2665252525', 'homero@simpson.com', 1);

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
(15, 'Adrian', 'Martinez', '1152535433', '34444333', 'adrian@maravilla.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reserva`
--

CREATE TABLE `reserva` (
  `idReserva` int(11) NOT NULL,
  `fechaEntrada` date NOT NULL,
  `fechaSalida` date NOT NULL,
  `estado` varchar(20) DEFAULT 'Vigente',
  `fechaMulta` date DEFAULT NULL,
  `multa` decimal(10,2) DEFAULT 0.00,
  `idInquilino` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `reserva`
--

INSERT INTO `reserva` (`idReserva`, `fechaEntrada`, `fechaSalida`, `estado`, `fechaMulta`, `multa`, `idInquilino`, `idInmueble`) VALUES
(2, '2026-09-20', '2026-09-22', '0', NULL, NULL, 4, 3),
(3, '2026-09-02', '2026-09-04', '0', NULL, NULL, 4, 4),
(4, '2026-09-06', '2026-09-08', '1', NULL, NULL, 3, 1),
(5, '2026-09-09', '2026-09-04', '1', NULL, NULL, 4, 4);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `rol`
--

CREATE TABLE `rol` (
  `idRol` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
(1, 'casa'),
(2, 'departamento'),
(3, 'quinta');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `contrasena` varchar(255) NOT NULL,
  `estado` tinyint(1) DEFAULT 1,
  `avatar` varchar(255) DEFAULT NULL,
  `idRol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  MODIFY `idImagen` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `reserva`
--
ALTER TABLE `reserva`
  MODIFY `idReserva` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `rol`
--
ALTER TABLE `rol`
  MODIFY `idRol` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  MODIFY `idTipoInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT;

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
