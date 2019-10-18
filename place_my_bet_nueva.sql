-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1:3306
-- Tiempo de generación: 18-10-2019 a las 17:29:57
-- Versión del servidor: 5.7.26
-- Versión de PHP: 7.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `place_my_bet`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `apuesta`
--

DROP TABLE IF EXISTS `apuesta`;
CREATE TABLE IF NOT EXISTS `apuesta` (
  `ID_APUESTA` int(11) NOT NULL AUTO_INCREMENT,
  `ES_UNDER` tinyint(1) NOT NULL,
  `CUOTA` double NOT NULL,
  `DINERO` double NOT NULL,
  `ID_MERCADO` int(11) NOT NULL,
  `EMAIL_USUARIO` varchar(50) NOT NULL,
  PRIMARY KEY (`ID_APUESTA`),
  KEY `ID_MERCADO` (`ID_MERCADO`),
  KEY `EMAIL_USUARIO` (`EMAIL_USUARIO`)
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `apuesta`
--

INSERT INTO `apuesta` (`ID_APUESTA`, `ES_UNDER`, `CUOTA`, `DINERO`, `ID_MERCADO`, `EMAIL_USUARIO`) VALUES
(76, 1, 1.6, 250, 1, 'ARANTXA000@GMAIL.COM'),
(77, 0, 4.275, 75, 1, 'ARANTXA000@GMAIL.COM');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cuenta`
--

DROP TABLE IF EXISTS `cuenta`;
CREATE TABLE IF NOT EXISTS `cuenta` (
  `NUMERO_TARJETA` int(11) NOT NULL,
  `SALDO_ACTUAL` double NOT NULL,
  `NOMBRE_BANCO` varchar(50) NOT NULL,
  `EMAIL_USUARIO` varchar(50) NOT NULL,
  PRIMARY KEY (`NUMERO_TARJETA`),
  KEY `EMAIL_USUARIO` (`EMAIL_USUARIO`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `cuenta`
--

INSERT INTO `cuenta` (`NUMERO_TARJETA`, `SALDO_ACTUAL`, `NOMBRE_BANCO`, `EMAIL_USUARIO`) VALUES
(33445590, 12000, 'SANTANDER', 'ARANTXA000@GMAIL.COM'),
(52362591, 2500, 'BANESTO', 'MARCOSGOMEZ@GMAIL.COM');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `evento`
--

DROP TABLE IF EXISTS `evento`;
CREATE TABLE IF NOT EXISTS `evento` (
  `ID_EVENTO` int(11) NOT NULL AUTO_INCREMENT,
  `NOMBRE_EQUIPO_LOCAL` varchar(50) NOT NULL,
  `NOMBRE_EQUIPO_VISITANTE` varchar(50) NOT NULL,
  `INICIO_PARTIDO` datetime NOT NULL,
  `FIN_PARTIDO` datetime NOT NULL,
  PRIMARY KEY (`ID_EVENTO`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `evento`
--

INSERT INTO `evento` (`ID_EVENTO`, `NOMBRE_EQUIPO_LOCAL`, `NOMBRE_EQUIPO_VISITANTE`, `INICIO_PARTIDO`, `FIN_PARTIDO`) VALUES
(1, 'VALENCIA CF', 'OSASUNA', '2019-06-25 20:00:00', '2019-06-25 21:30:00'),
(2, 'Barcelona', 'Valladolid', '2019-09-01 13:00:00', '2019-09-01 14:00:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mercado`
--

DROP TABLE IF EXISTS `mercado`;
CREATE TABLE IF NOT EXISTS `mercado` (
  `ID_MERCADO` int(11) NOT NULL AUTO_INCREMENT,
  `TIPO` double NOT NULL,
  `CUOTA_OVER` double NOT NULL,
  `CUOTA_UNDER` double NOT NULL,
  `DINERO_APOSTADO_OVER` double NOT NULL,
  `DINERO_APOSTADO_UNDER` double NOT NULL,
  `ID_EVENTO` int(11) NOT NULL,
  PRIMARY KEY (`ID_MERCADO`),
  KEY `ID_EVENTO` (`ID_EVENTO`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `mercado`
--

INSERT INTO `mercado` (`ID_MERCADO`, `TIPO`, `CUOTA_OVER`, `CUOTA_UNDER`, `DINERO_APOSTADO_OVER`, `DINERO_APOSTADO_UNDER`, `ID_EVENTO`) VALUES
(1, 1.5, 2.8499999999999996, 1.4249999999999998, 175, 350, 1),
(2, 2.5, 1.5, 1.2, 100, 100, 1),
(9, 3.5, 1.5, 1.5, 100, 100, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE IF NOT EXISTS `usuario` (
  `EMAIL_USUARIO` varchar(50) NOT NULL,
  `NOMBRE` varchar(50) NOT NULL,
  `APELLIDOS` varchar(100) NOT NULL,
  `EDAD` int(3) NOT NULL,
  PRIMARY KEY (`EMAIL_USUARIO`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`EMAIL_USUARIO`, `NOMBRE`, `APELLIDOS`, `EDAD`) VALUES
('ARANTXA000@GMAIL.COM', 'ARANTXA', 'DE JUAN, GONZALEZ', 25),
('MARCOSGOMEZ@GMAIL.COM', 'MARCOS', 'GOMEZ SANCHEZ', 33),
('PEPEPEREZ2000@GMAIL.COM', 'PEPE', 'PEREZ, MARIN', 42);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
