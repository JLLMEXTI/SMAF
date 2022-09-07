/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : inapesca_cripsc

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-01-20 10:36:51
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `crip_area`
-- ----------------------------
DROP TABLE IF EXISTS `crip_area`;
CREATE TABLE `crip_area` (
  `CLV_AREA` varchar(8) NOT NULL DEFAULT '',
  `DESCRIPCION` varchar(50) DEFAULT NULL,
  `CLV_DEP` varchar(8) NOT NULL,
  PRIMARY KEY (`CLV_AREA`),
  KEY `CLV_AREA` (`CLV_AREA`),
  KEY `CLV_DEP` (`CLV_DEP`),
  CONSTRAINT `crip_area_ibfk_1` FOREIGN KEY (`CLV_DEP`) REFERENCES `crip_dependencia` (`CLV_DEP`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_area
-- ----------------------------
INSERT INTO `crip_area` VALUES ('CRIPSC01', 'ADMINISTRACION', 'CRIP-SC');
INSERT INTO `crip_area` VALUES ('CRIPSC02', 'TIBURON', 'CRIP-SC');
INSERT INTO `crip_area` VALUES ('CRIPSC03', 'ACUACULTURA', 'CRIP-SC');
INSERT INTO `crip_area` VALUES ('CRIPSC04', 'TECNOLOGIAS DE CAPTURAS', 'CRIP-SC');
INSERT INTO `crip_area` VALUES ('CRIPSC05', 'PESCA RIBEREÑA', 'CRIP-SC');
INSERT INTO `crip_area` VALUES ('CRIPSC06', 'CAMARON', 'CRIP-SC');
INSERT INTO `crip_area` VALUES ('CRIPSC07', 'TECNOLOGIA INDUSTRIAL', 'CRIP-SC');
INSERT INTO `crip_area` VALUES ('CRIPSC08', 'JEFATURA DE CENTRO', 'CRIP-SC');

-- ----------------------------
-- Table structure for `crip_ciudad`
-- ----------------------------
DROP TABLE IF EXISTS `crip_ciudad`;
CREATE TABLE `crip_ciudad` (
  `CLV_CIUDAD` varchar(5) NOT NULL,
  `DESCR` varchar(100) NOT NULL,
  `NOM_CORTO` varchar(100) NOT NULL,
  `CLV_ESTADO` varchar(3) NOT NULL,
  PRIMARY KEY (`CLV_CIUDAD`,`CLV_ESTADO`),
  KEY `CLV_ESTADO` (`CLV_ESTADO`),
  KEY `CLV_CIUDAD` (`CLV_CIUDAD`),
  CONSTRAINT `crip_ciudad_ibfk_1` FOREIGN KEY (`CLV_ESTADO`) REFERENCES `crip_estado` (`CLV_ESTADO`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_ciudad
-- ----------------------------
INSERT INTO `crip_ciudad` VALUES ('SC', 'SALINA CRUZ', 'SALINA CRUZ', '20');
INSERT INTO `crip_ciudad` VALUES ('TH', 'SANTO DOMINGO TEHUANTEPEC', 'TEHUANTEPEC', '20');

-- ----------------------------
-- Table structure for `crip_comision`
-- ----------------------------
DROP TABLE IF EXISTS `crip_comision`;
CREATE TABLE `crip_comision` (
  `CVL_OFICIO` varchar(8) NOT NULL,
  `FOLIO` int(6) NOT NULL,
  `NO_OFICIO` int(6) DEFAULT NULL,
  `FECHA_SOL` date NOT NULL,
  `FECHA_RESP` date DEFAULT NULL,
  `FECHA_VoBo` date DEFAULT NULL,
  `FECHA_AUTORIZA` date DEFAULT NULL,
  `USSER` varchar(255) DEFAULT NULL,
  `CLV_DEP` varchar(8) NOT NULL,
  `CLV_AREA` varchar(8) NOT NULL,
  `CLV_PROY` varchar(10) NOT NULL,
  `LUGAR` varchar(255) NOT NULL,
  `TELEFONO` int(15) DEFAULT NULL,
  `CAPITULO` varchar(50) DEFAULT NULL,
  `PROCESO` varchar(50) DEFAULT NULL,
  `INDICADOR` varchar(50) DEFAULT NULL,
  `PART_PRESUPUESTAL` varchar(50) DEFAULT NULL,
  `FECHA_I` date NOT NULL,
  `FECHA_F` date NOT NULL,
  `DIAS_TOTAL` int(2) DEFAULT NULL,
  `DIAS_REALES` double(2,1) DEFAULT NULL,
  `OBJETIVO` varchar(255) DEFAULT NULL,
  `CLV_CLASE` varchar(3) NOT NULL,
  `CLV_TIPO_TRANS` varchar(2) NOT NULL,
  `CLV_TRANS-SOL` varchar(4) NOT NULL,
  `CLV_TRANS_AUT` varchar(4) NOT NULL,
  `DESC_AUTO` varchar(255) DEFAULT NULL,
  `VUELO_PARTIDA_NUM` varchar(15) DEFAULT NULL,
  `FECHA_VUELO_IDA` date DEFAULT NULL,
  `HORA_PART` time DEFAULT NULL,
  `VUELO_RETURN_NUM` varchar(255) DEFAULT NULL,
  `FECHA_VUELO_RETURN` date DEFAULT NULL,
  `HORA_RET` time DEFAULT NULL,
  `COMBUSTIBLE_SOL` double(5,2) DEFAULT NULL,
  `COMBUSTIBLE_AUT` double(5,2) DEFAULT NULL,
  `PARTIDA_COMBUSTIBLE` varchar(10) DEFAULT NULL,
  `MET_VIATICOS` varchar(2) DEFAULT NULL,
  `SECEFF` int(2) NOT NULL DEFAULT '0',
  `EQUIPO` varchar(255) DEFAULT NULL,
  `OBSERVACIONES_SOL` varchar(255) DEFAULT NULL,
  `RESP_PROY` varchar(25) NOT NULL,
  `VoBo` varchar(150) DEFAULT NULL,
  `AUTORIZA` varchar(150) DEFAULT NULL,
  `USUARIO` varchar(25) NOT NULL,
  `CLV_DEP_COM` varchar(8) NOT NULL,
  `ESTATUS` int(1) NOT NULL,
  `OBSERVACIONES_VoBo` varchar(255) DEFAULT NULL,
  `OBSERVACIONES_AUT` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`CVL_OFICIO`,`FOLIO`,`SECEFF`),
  KEY `ESTATUS` (`ESTATUS`),
  KEY `MET_VIATICOS` (`MET_VIATICOS`),
  KEY `USUARIO` (`USUARIO`),
  KEY `CLV_AREA` (`CLV_AREA`),
  KEY `CLV_PROY` (`CLV_PROY`),
  KEY `RESP_PROY` (`RESP_PROY`),
  KEY `FOLIO` (`FOLIO`),
  KEY `FECHA_I` (`FECHA_I`),
  KEY `FECHA_F` (`FECHA_F`),
  KEY `CLV_CLASE` (`CLV_CLASE`),
  KEY `CLV_TIPO_TRANS` (`CLV_TIPO_TRANS`),
  KEY `CLV_TRANS-SOL` (`CLV_TRANS-SOL`),
  KEY `CLV_TRANS_AUT` (`CLV_TRANS_AUT`),
  KEY `USSER` (`USSER`),
  KEY `CLV_DEP` (`CLV_DEP`),
  CONSTRAINT `crip_comision_ibfk_1` FOREIGN KEY (`CVL_OFICIO`) REFERENCES `crip_oficio` (`CLV_TIPO_O`),
  CONSTRAINT `crip_comision_ibfk_11` FOREIGN KEY (`MET_VIATICOS`) REFERENCES `crip_mviaticos` (`CLV_MET`),
  CONSTRAINT `crip_comision_ibfk_12` FOREIGN KEY (`USUARIO`) REFERENCES `crip_usuarios` (`USUARIO`),
  CONSTRAINT `crip_comision_ibfk_14` FOREIGN KEY (`CLV_AREA`) REFERENCES `crip_area` (`CLV_AREA`),
  CONSTRAINT `crip_comision_ibfk_15` FOREIGN KEY (`CLV_PROY`) REFERENCES `crip_proy` (`CLV-PROY`),
  CONSTRAINT `crip_comision_ibfk_17` FOREIGN KEY (`CLV_CLASE`) REFERENCES `crip_transporte` (`CLV_CLASE`),
  CONSTRAINT `crip_comision_ibfk_18` FOREIGN KEY (`CLV_TIPO_TRANS`) REFERENCES `crip_transporte` (`TIPO`),
  CONSTRAINT `crip_comision_ibfk_19` FOREIGN KEY (`CLV_TRANS-SOL`) REFERENCES `crip_transporte` (`CLV_TRANSPORTE`),
  CONSTRAINT `crip_comision_ibfk_21` FOREIGN KEY (`USSER`) REFERENCES `crip_usuarios` (`USUARIO`),
  CONSTRAINT `crip_comision_ibfk_22` FOREIGN KEY (`CLV_DEP`) REFERENCES `crip_dependencia` (`CLV_DEP`),
  CONSTRAINT `crip_comision_ibfk_23` FOREIGN KEY (`RESP_PROY`) REFERENCES `crip_proy` (`RESPONSABLE`),
  CONSTRAINT `crip_comision_ibfk_5` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_comision
-- ----------------------------

-- ----------------------------
-- Table structure for `crip_dependencia`
-- ----------------------------
DROP TABLE IF EXISTS `crip_dependencia`;
CREATE TABLE `crip_dependencia` (
  `CLV_DEP` varchar(8) NOT NULL,
  `DESCRIPCION` varchar(100) NOT NULL,
  `DESCR_CORTO` varchar(50) NOT NULL,
  `CLV_SECRE` varchar(15) NOT NULL,
  `CLV_ORG` varchar(10) NOT NULL,
  `CLV_DIR` varchar(10) NOT NULL,
  `CALLE` varchar(100) DEFAULT NULL,
  `NUM_EXT` varchar(5) DEFAULT NULL,
  `NUM_INT` varchar(5) DEFAULT NULL,
  `COL` varchar(100) DEFAULT NULL,
  `C.P.` varchar(7) DEFAULT NULL,
  `CIUDAD` varchar(100) DEFAULT NULL,
  `CLV_ESTADO` varchar(3) NOT NULL,
  `CLV_PAIS` varchar(5) NOT NULL,
  `TEL_1` varchar(15) DEFAULT NULL,
  `TEL_2` varchar(15) DEFAULT NULL,
  `FAX` varchar(15) DEFAULT NULL,
  `EMAIL_1` varchar(40) DEFAULT NULL,
  `EMAIL_2` varchar(40) DEFAULT NULL,
  `FECHA` date NOT NULL,
  `FECH_EFF` date NOT NULL,
  `SECEFF` int(4) NOT NULL,
  `ESTATUS` int(1) NOT NULL,
  PRIMARY KEY (`CLV_DEP`,`CLV_SECRE`,`CLV_ORG`,`SECEFF`),
  KEY `CLV_DEP` (`CLV_DEP`),
  KEY `CLV_SECRE_2` (`CLV_SECRE`),
  KEY `CLV_ORG` (`CLV_ORG`),
  KEY `CLV_DIR` (`CLV_DIR`),
  CONSTRAINT `crip_dependencia_ibfk_1` FOREIGN KEY (`CLV_SECRE`) REFERENCES `crip_secretaria` (`CLV_SECRETARIA`),
  CONSTRAINT `crip_dependencia_ibfk_2` FOREIGN KEY (`CLV_ORG`) REFERENCES `crip_organismos` (`CLV_ORG`),
  CONSTRAINT `crip_dependencia_ibfk_3` FOREIGN KEY (`CLV_DIR`) REFERENCES `crip_direcciones` (`CLV_DIR`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_dependencia
-- ----------------------------
INSERT INTO `crip_dependencia` VALUES ('AGUAIT', 'RECURSOS HIDRAULICOS ISTMO', 'RECURSOS HIDRAULICOS ISTMO', 'SAGARPA', 'CONAGUA', 'DGAIPP', null, null, null, null, null, null, '', '', null, null, null, null, null, '2014-01-01', '2014-12-03', '1', '1');
INSERT INTO `crip_dependencia` VALUES ('CRIP-MC', 'CENTRO REGIONAL DE INVESTIGACION PESQUERA MICHOACAN', 'CRIP MICHOACAN', 'SAGARPA', 'INAPESCA', 'DGAIPP', null, null, null, null, null, null, '', '', null, null, null, null, null, '2014-01-01', '0000-00-00', '1', '1');
INSERT INTO `crip_dependencia` VALUES ('CRIP-MZ', 'CENTRO REGIONAL DE INVESTIGACION PESQUERA MANZANILLO', 'CRIP MANZANILLO', 'SAGARPA', 'INAPESCA', 'DGAIPP', 'PROLONGACION PLAYA ABIERTA', 'S/N', null, 'MIRAMAR', '7680', 'MANZANILLO', '20', 'MEX', '5555555555', '6666666666', null, null, null, '2014-12-08', '2014-12-03', '1', '1');
INSERT INTO `crip_dependencia` VALUES ('CRIP-SC', 'CENTRO REGIONAL DE INVESTIGACION PESQUERA SALINA CRUZ', 'CRIP SALINA CRUZ', 'SAGARPA', 'INAPESCA', 'DGAIPP', 'PROLONGACION PLAYA ABIERTA', 'S/N', null, 'MIRAMAR', '7680', 'SALINA CRUZ', '20', 'MEX', '9717145003', '9717140386', null, 'admon_ps@prodigy.net.mx', 'cripsc@prodigy.net.mx', '1900-01-01', '2014-11-13', '1', '1');

-- ----------------------------
-- Table structure for `crip_direcciones`
-- ----------------------------
DROP TABLE IF EXISTS `crip_direcciones`;
CREATE TABLE `crip_direcciones` (
  `CLV_DIR` varchar(10) NOT NULL,
  `DESCRIPCION` varchar(100) NOT NULL,
  `DESC_CORTA` varchar(50) DEFAULT NULL,
  `CLV_ORG` varchar(10) NOT NULL,
  `ESTATUS` int(1) NOT NULL,
  `SEC_EFF` int(3) NOT NULL,
  `DATE` date DEFAULT NULL,
  `DATE_EFF` date DEFAULT NULL,
  PRIMARY KEY (`CLV_DIR`),
  KEY `CLV_ORG` (`CLV_ORG`),
  KEY `ESTATUS` (`ESTATUS`),
  CONSTRAINT `crip_direcciones_ibfk_1` FOREIGN KEY (`CLV_ORG`) REFERENCES `crip_organismos` (`CLV_ORG`),
  CONSTRAINT `crip_direcciones_ibfk_2` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_direcciones
-- ----------------------------
INSERT INTO `crip_direcciones` VALUES ('DGAIPP', 'DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA DEL PACIFICO', 'DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUER', 'INAPESCA', '1', '1', '2014-12-26', '2014-12-26');

-- ----------------------------
-- Table structure for `crip_estado`
-- ----------------------------
DROP TABLE IF EXISTS `crip_estado`;
CREATE TABLE `crip_estado` (
  `CLV_ESTADO` varchar(3) NOT NULL,
  `DESCR` varchar(50) NOT NULL,
  `NOM_CORTO` varchar(50) NOT NULL,
  `CLV_PAIS` varchar(3) NOT NULL,
  PRIMARY KEY (`CLV_ESTADO`,`CLV_PAIS`),
  KEY `CLV_PAIS` (`CLV_PAIS`),
  KEY `CLV_ESTADO` (`CLV_ESTADO`),
  CONSTRAINT `CLV_PAIS` FOREIGN KEY (`CLV_PAIS`) REFERENCES `crip_pais` (`CLV_PAIS`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_estado
-- ----------------------------
INSERT INTO `crip_estado` VALUES ('20', 'OAXACA', 'OAXACA', 'MEX');

-- ----------------------------
-- Table structure for `crip_estatus`
-- ----------------------------
DROP TABLE IF EXISTS `crip_estatus`;
CREATE TABLE `crip_estatus` (
  `CLV_ESTATUS` int(1) NOT NULL,
  `DESCRIPCION` varchar(100) NOT NULL,
  PRIMARY KEY (`CLV_ESTATUS`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_estatus
-- ----------------------------
INSERT INTO `crip_estatus` VALUES ('0', 'INACTIVO');
INSERT INTO `crip_estatus` VALUES ('1', 'ACTIVO');
INSERT INTO `crip_estatus` VALUES ('2', 'CANCELADO');
INSERT INTO `crip_estatus` VALUES ('3', 'RECHAZADO');
INSERT INTO `crip_estatus` VALUES ('4', 'EN TRAMITE DE BAJA');

-- ----------------------------
-- Table structure for `crip_etq`
-- ----------------------------
DROP TABLE IF EXISTS `crip_etq`;
CREATE TABLE `crip_etq` (
  `CLV_ETQ` int(6) NOT NULL,
  `ETQ_ESP` varchar(255) NOT NULL,
  `ETQ_POR` varchar(255) DEFAULT NULL,
  `ETQ_ENG` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`CLV_ETQ`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_etq
-- ----------------------------
INSERT INTO `crip_etq` VALUES ('1', 'INSTITUTO NACIONAL DE PESCA', null, null);
INSERT INTO `crip_etq` VALUES ('2', 'DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL PACIFICO', null, null);
INSERT INTO `crip_etq` VALUES ('3', 'CENTRO REGIONAL DE INVESIGACION PESQUERA DE SALINA CRUZ, OAX.', null, null);
INSERT INTO `crip_etq` VALUES ('4', 'SOLICITUD DE ', null, null);
INSERT INTO `crip_etq` VALUES ('5', 'COMISION', null, null);
INSERT INTO `crip_etq` VALUES ('6', 'ADQUISICION DE MATERIALES', null, null);
INSERT INTO `crip_etq` VALUES ('7', 'SERVICIO', null, null);
INSERT INTO `crip_etq` VALUES ('8', 'ORDEN DE', null, null);
INSERT INTO `crip_etq` VALUES ('9', 'FECHA', null, null);
INSERT INTO `crip_etq` VALUES ('10', 'FOLIO', null, null);
INSERT INTO `crip_etq` VALUES ('11', 'AREA SOLICITANTE', null, null);
INSERT INTO `crip_etq` VALUES ('12', 'PROYECTO', null, null);
INSERT INTO `crip_etq` VALUES ('13', 'NOMBRE ', null, null);
INSERT INTO `crip_etq` VALUES ('14', 'DEL', null, null);
INSERT INTO `crip_etq` VALUES ('15', 'COMISIONADO', null, null);
INSERT INTO `crip_etq` VALUES ('16', '(S)', null, null);
INSERT INTO `crip_etq` VALUES ('17', '(OS)', null, null);
INSERT INTO `crip_etq` VALUES ('18', 'LOS', null, null);
INSERT INTO `crip_etq` VALUES ('19', 'DE', null, null);
INSERT INTO `crip_etq` VALUES ('20', 'LUGAR', null, null);
INSERT INTO `crip_etq` VALUES ('21', '(ES)', null, null);
INSERT INTO `crip_etq` VALUES ('22', 'AL', null, null);
INSERT INTO `crip_etq` VALUES ('23', 'OBJETIVO', null, null);
INSERT INTO `crip_etq` VALUES ('24', 'MEDIO', null, null);
INSERT INTO `crip_etq` VALUES ('25', 'TRANSPORTE', null, null);
INSERT INTO `crip_etq` VALUES ('26', 'TERRESTRE', null, null);
INSERT INTO `crip_etq` VALUES ('27', 'AEREO', null, null);
INSERT INTO `crip_etq` VALUES ('28', 'REQUERIMIENTO', null, null);
INSERT INTO `crip_etq` VALUES ('29', 'COMBUSTIBLE', null, null);
INSERT INTO `crip_etq` VALUES ('30', 'EQUIPO', null, null);
INSERT INTO `crip_etq` VALUES ('31', 'A', null, null);
INSERT INTO `crip_etq` VALUES ('32', 'UTILIZAR', null, null);
INSERT INTO `crip_etq` VALUES ('33', 'OBSERVACIONES', null, null);
INSERT INTO `crip_etq` VALUES ('34', 'SOLICITA', null, null);
INSERT INTO `crip_etq` VALUES ('35', 'Vo. Bo.', null, null);
INSERT INTO `crip_etq` VALUES ('36', 'AUTORIZA', null, null);
INSERT INTO `crip_etq` VALUES ('37', 'RESPONSABLE', null, null);
INSERT INTO `crip_etq` VALUES ('38', 'ADMINISTRADOR', null, null);
INSERT INTO `crip_etq` VALUES ('39', 'CRIP SC', null, null);
INSERT INTO `crip_etq` VALUES ('40', 'JEFE', null, null);
INSERT INTO `crip_etq` VALUES ('41', 'NOTAS', null, null);
INSERT INTO `crip_etq` VALUES ('42', 'IMPORTANTES', null, null);
INSERT INTO `crip_etq` VALUES ('43', ':', null, null);
INSERT INTO `crip_etq` VALUES ('44', '1.-', null, null);
INSERT INTO `crip_etq` VALUES ('45', '2.-', null, null);
INSERT INTO `crip_etq` VALUES ('46', '3.-', null, null);
INSERT INTO `crip_etq` VALUES ('47', '4.-', null, null);
INSERT INTO `crip_etq` VALUES ('48', '5.-', null, null);
INSERT INTO `crip_etq` VALUES ('49', 'INDICAR HORARIOS DE VUELOS DE ACUERDO A SUS PROGRAMAS DE TRABAJO.', null, null);
INSERT INTO `crip_etq` VALUES ('50', 'EN EL COMBUSTIBLE DESGLOSAR LO QUE UTILIZARA EN VEHÍCULO Y LANCHA CUANDO SEA EL CASO.', null, null);
INSERT INTO `crip_etq` VALUES ('51', 'ESPECIFICAR DIAS DE TRABAJO A BORDO DE EMBARCACIONES CUANDO SEA EL CASO.', null, null);
INSERT INTO `crip_etq` VALUES ('52', 'ENTREGA', null, null);
INSERT INTO `crip_etq` VALUES ('53', 'BIENES', null, null);
INSERT INTO `crip_etq` VALUES ('54', 'PROCESO', null, null);
INSERT INTO `crip_etq` VALUES ('55', 'INDICADOR', null, null);
INSERT INTO `crip_etq` VALUES ('56', 'TELEFONO', null, null);
INSERT INTO `crip_etq` VALUES ('57', 'PARTIDA', null, null);
INSERT INTO `crip_etq` VALUES ('58', 'PRESUPUESTAL', null, null);
INSERT INTO `crip_etq` VALUES ('59', 'No.', null, null);
INSERT INTO `crip_etq` VALUES ('60', 'DESCRIPCION', null, null);
INSERT INTO `crip_etq` VALUES ('61', 'ARTICULO', null, null);
INSERT INTO `crip_etq` VALUES ('62', 'CANTIDAD', null, null);
INSERT INTO `crip_etq` VALUES ('63', 'DEPOSITARIO', null, null);
INSERT INTO `crip_etq` VALUES ('64', 'JUSTIFICACION', null, null);
INSERT INTO `crip_etq` VALUES ('65', 'CAPITULO', null, null);
INSERT INTO `crip_etq` VALUES ('66', 'S', null, null);
INSERT INTO `crip_etq` VALUES ('67', 'USUARIO', null, null);
INSERT INTO `crip_etq` VALUES ('68', 'NOTIFICACION', null, null);
INSERT INTO `crip_etq` VALUES ('69', 'OFICIO', null, null);

-- ----------------------------
-- Table structure for `crip_formularios`
-- ----------------------------
DROP TABLE IF EXISTS `crip_formularios`;
CREATE TABLE `crip_formularios` (
  `MODULO` varchar(30) NOT NULL,
  `DESCRIPCION` varchar(100) NOT NULL,
  `PADRE` varchar(30) NOT NULL,
  `ROL` varchar(9) NOT NULL,
  PRIMARY KEY (`MODULO`),
  KEY `MODULO` (`MODULO`),
  KEY `PADRE` (`PADRE`),
  KEY `ROL` (`ROL`),
  CONSTRAINT `crip_formularios_ibfk_1` FOREIGN KEY (`ROL`) REFERENCES `crip_roles` (`CLV_ROL`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_formularios
-- ----------------------------
INSERT INTO `crip_formularios` VALUES ('1', 'SOLICITUDES', '0', 'INVEST');
INSERT INTO `crip_formularios` VALUES ('11', 'SOLICITUD DE COMISION', '1', 'INVEST');
INSERT INTO `crip_formularios` VALUES ('12', 'SOLICITUD DE SERVICIO', '1', 'INVEST');
INSERT INTO `crip_formularios` VALUES ('13', 'SOLICITUD DE COMPRA', '1', 'INVEST');
INSERT INTO `crip_formularios` VALUES ('2', 'REVISIONES', '0', 'INVEST');

-- ----------------------------
-- Table structure for `crip_job`
-- ----------------------------
DROP TABLE IF EXISTS `crip_job`;
CREATE TABLE `crip_job` (
  `USUARIO` varchar(25) NOT NULL DEFAULT '',
  `PASSWORD` varchar(100) NOT NULL,
  `EMAIL_INST` varchar(100) NOT NULL,
  `CARGO` varchar(50) NOT NULL,
  `CLV_NIVEL` varchar(15) NOT NULL,
  `CLV_PLAZA` varchar(10) NOT NULL DEFAULT '',
  `CLV_PUESTO` varchar(100) NOT NULL,
  `CLV_SECRE` varchar(15) NOT NULL,
  `CLV_ORG` varchar(10) NOT NULL,
  `CLV_DEP` varchar(8) NOT NULL,
  `CLV_AREA` varchar(8) NOT NULL,
  `CLV_PROY` varchar(10) NOT NULL,
  `FECH_ALTA` date NOT NULL,
  `FECH_EFF` date NOT NULL,
  `ESTATUS` int(1) NOT NULL,
  `SEC_EFF` int(6) NOT NULL,
  `ROL` varchar(9) NOT NULL,
  PRIMARY KEY (`USUARIO`,`CLV_NIVEL`,`CLV_PLAZA`,`CLV_PUESTO`,`CLV_PROY`,`SEC_EFF`),
  KEY `USUARIO` (`USUARIO`),
  KEY `CLV_AREA` (`CLV_AREA`),
  KEY `CLV_PROY` (`CLV_PROY`),
  KEY `CLV_DEP` (`CLV_SECRE`),
  KEY `CLV_ORG` (`CLV_ORG`),
  KEY `ESTATUS` (`ESTATUS`),
  KEY `CLV_NIVEL` (`CLV_NIVEL`) USING BTREE,
  KEY `CLV_PUESTO` (`CLV_PUESTO`),
  KEY `CLV_PLAZA` (`CLV_PLAZA`),
  KEY `DEPENDENCIA` (`CLV_DEP`),
  KEY `ROL` (`ROL`),
  CONSTRAINT `CLV_ORG` FOREIGN KEY (`CLV_ORG`) REFERENCES `crip_organismos` (`CLV_ORG`),
  CONSTRAINT `CLV_SECRE` FOREIGN KEY (`CLV_SECRE`) REFERENCES `crip_secretaria` (`CLV_SECRETARIA`),
  CONSTRAINT `CLV_USUARIOS` FOREIGN KEY (`USUARIO`) REFERENCES `crip_usuarios` (`USUARIO`),
  CONSTRAINT `crip_job_ibfk_1` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`),
  CONSTRAINT `crip_job_ibfk_2` FOREIGN KEY (`CLV_NIVEL`) REFERENCES `crip_puestos` (`CLV_NIVEL`),
  CONSTRAINT `crip_job_ibfk_3` FOREIGN KEY (`CLV_PUESTO`) REFERENCES `crip_puestos` (`CLV_PUESTO`),
  CONSTRAINT `crip_job_ibfk_4` FOREIGN KEY (`CLV_PLAZA`) REFERENCES `crip_puestos` (`CLV_PLAZA`),
  CONSTRAINT `crip_job_ibfk_5` FOREIGN KEY (`CLV_AREA`) REFERENCES `crip_area` (`CLV_AREA`),
  CONSTRAINT `crip_job_ibfk_6` FOREIGN KEY (`CLV_PROY`) REFERENCES `crip_proy` (`CLV-PROY`),
  CONSTRAINT `crip_job_ibfk_7` FOREIGN KEY (`ROL`) REFERENCES `crip_roles` (`CLV_ROL`),
  CONSTRAINT `DEPENDENCIA` FOREIGN KEY (`CLV_DEP`) REFERENCES `crip_dependencia` (`CLV_DEP`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_job
-- ----------------------------
INSERT INTO `crip_job` VALUES ('JLLMEXTI', 'BoUYk3osy1lGb2vBSlWNsw==', 'juan.llopez@inapesca.gob.mx', 'SISTEMAS', 'PQ2', '0506', 'ENLACE', 'SAGARPA', 'INAPESCA', 'CRIP-SC', 'CRIPSC01', 'CRIPSC000', '2014-10-16', '2014-12-10', '1', '1', 'INVEST');
INSERT INTO `crip_job` VALUES ('LACA831007AT1', '4dff5742a6568353b0702dbe48acd299', 'juan.llopez@inapesca.gob.mx', 'INVESTIGADOR', 'E3421', '0434', 'TECNICO ASOCIADO \"A\"', 'SAGARPA', 'INAPESCA', 'CRIP-SC', 'CRIPSC05', 'CRIPSC008', '2014-01-01', '2014-12-10', '1', '1', 'INVEST');
INSERT INTO `crip_job` VALUES ('LIRA890424R61', '4dff5742a6568353b0702dbe48acd299', 'juan.llopez@inapesca.gob.mx', 'INVESTIGADOR', 'E3423', '9012', 'TECNICO DE APOYO EN INVESTIGACION', 'SAGARPA', 'INAPESCA', 'CRIP-SC', 'CRIPSC02', 'CRIPSC002', '2014-12-10', '2014-12-10', '1', '1', 'INVEST');
INSERT INTO `crip_job` VALUES ('MOPO670124CX7', '4dff5742a6568353b0702dbe48acd299', 'juan.llopez@inapesca.gob.mx', 'JEFE DE CENTRO', 'CFNA001', '0363', 'SUBDIRECTOR DE AREA', 'SAGARPA', 'INAPESCA', 'CRIP-SC', 'CRIPSC08', 'CRIPSC000', '2014-12-23', '2014-12-23', '1', '1', 'INVEST');
INSERT INTO `crip_job` VALUES ('MOPO670124CX7', '4dff5742a6568353b0702dbe48acd299', 'juan.llopez@inapesca.gob.mx', 'JEFE DE CENTRO', 'CFNA001', '0363', 'SUBDIRECTOR DE AREA', 'SAGARPA', 'INAPESCA', 'CRIP-SC', 'CRIPSC08', 'CRIPSC002', '2014-12-01', '2014-12-10', '1', '1', 'INVEST');
INSERT INTO `crip_job` VALUES ('NUOA7803309D6', '4dff5742a6568353b0702dbe48acd299', 'juan.llopez@inapesca.gob.mx', 'INVESTIGADOR', 'E3429', '0470', 'INVESTIGADOR ASOCIADO \"A\"', 'SAGARPA', 'INAPESCA', 'CRIP-MZ', 'CRIPSC06', 'CRIPSC007', '2014-01-01', '2014-12-10', '1', '1', 'INVEST');
INSERT INTO `crip_job` VALUES ('VEML590903D66', '4dff5742a6568353b0702dbe48acd299', 'juan.llopez@inapesca.gob.mx', 'ADMINISTRADOR', 'CFPQ2', '0480', 'PROFESIONAL DICTAMINADOR DE SERVICIOS ESP.', 'SAGARPA', 'INAPESCA', 'CRIP-SC', 'CRIPSC01', 'CRIPSC000', '2014-12-16', '2014-12-16', '1', '1', 'INVEST');

-- ----------------------------
-- Table structure for `crip_mviaticos`
-- ----------------------------
DROP TABLE IF EXISTS `crip_mviaticos`;
CREATE TABLE `crip_mviaticos` (
  `CLV_MET` varchar(2) NOT NULL DEFAULT '',
  `DESCR` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`CLV_MET`),
  KEY `CLV_MET` (`CLV_MET`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_mviaticos
-- ----------------------------
INSERT INTO `crip_mviaticos` VALUES ('0', 'NO APLICA');
INSERT INTO `crip_mviaticos` VALUES ('1', 'DEVENGADOS');
INSERT INTO `crip_mviaticos` VALUES ('2', 'DEPOSITO');

-- ----------------------------
-- Table structure for `crip_oficio`
-- ----------------------------
DROP TABLE IF EXISTS `crip_oficio`;
CREATE TABLE `crip_oficio` (
  `CLV_TIPO_O` varchar(8) NOT NULL DEFAULT '',
  `DESCRIPCION` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`CLV_TIPO_O`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_oficio
-- ----------------------------
INSERT INTO `crip_oficio` VALUES ('CRIPSC01', 'SOLICITUD DE COMISION');
INSERT INTO `crip_oficio` VALUES ('CRIPSC02', 'REQUISICION DE COMPRA');
INSERT INTO `crip_oficio` VALUES ('CRIPSC03', 'ORDEN DE SERVICIO');
INSERT INTO `crip_oficio` VALUES ('CRIPSC04', 'ACTA CIRCUNSTANCIADA');
INSERT INTO `crip_oficio` VALUES ('CRIPSC05', 'PROGRAMA DE TRABAJO');
INSERT INTO `crip_oficio` VALUES ('CRIPSC06', 'CONSTANCIA O DIPLOMA DE PARTICIPACION');
INSERT INTO `crip_oficio` VALUES ('CRIPSC07', 'OTROS');

-- ----------------------------
-- Table structure for `crip_organismos`
-- ----------------------------
DROP TABLE IF EXISTS `crip_organismos`;
CREATE TABLE `crip_organismos` (
  `CLV_ORG` varchar(10) NOT NULL,
  `DESCRIP` varchar(100) NOT NULL,
  `NOMBRE CORTO` varchar(100) NOT NULL,
  `ESTATUS` int(1) NOT NULL,
  `FECHA` date NOT NULL,
  `FECH_EFF` date NOT NULL,
  `SECEFF` int(11) NOT NULL,
  `CLV_SECRETARIA` varchar(15) NOT NULL,
  PRIMARY KEY (`CLV_ORG`,`SECEFF`,`CLV_SECRETARIA`),
  KEY `CLV_ORG` (`CLV_ORG`),
  KEY `CLV_SECRETARIA` (`CLV_SECRETARIA`),
  KEY `ESTATUS` (`ESTATUS`),
  CONSTRAINT `crip_organismos_ibfk_1` FOREIGN KEY (`CLV_SECRETARIA`) REFERENCES `crip_secretaria` (`CLV_SECRETARIA`),
  CONSTRAINT `crip_organismos_ibfk_2` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_organismos
-- ----------------------------
INSERT INTO `crip_organismos` VALUES ('CONAGUA', 'COMISION NACIONAL DEL AGUA', 'COMISIION NACIONAL DEL AGUA', '0', '2014-12-03', '2014-12-03', '1', 'SAGARPA');
INSERT INTO `crip_organismos` VALUES ('INAPESCA', 'INSTITUTO NACIONAL DE PESCA', 'INSTITUTO NACIONALDE PESCA', '1', '2014-11-02', '2014-11-02', '1', 'SAGARPA');

-- ----------------------------
-- Table structure for `crip_pais`
-- ----------------------------
DROP TABLE IF EXISTS `crip_pais`;
CREATE TABLE `crip_pais` (
  `CLV_PAIS` varchar(3) NOT NULL,
  `DESCR` varchar(50) NOT NULL,
  `NOM_CORTO` varchar(50) NOT NULL,
  PRIMARY KEY (`CLV_PAIS`),
  KEY `CLV_PAIS` (`CLV_PAIS`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_pais
-- ----------------------------
INSERT INTO `crip_pais` VALUES ('MEX', 'ESTADOS UNIDOS MEXICANOS', 'MEXICO');

-- ----------------------------
-- Table structure for `crip_partidas`
-- ----------------------------
DROP TABLE IF EXISTS `crip_partidas`;
CREATE TABLE `crip_partidas` (
  `ID` varchar(6) NOT NULL,
  `DESCRIPCION` varchar(255) NOT NULL,
  `PADRE` varchar(6) NOT NULL,
  `ESTATUS` int(1) NOT NULL DEFAULT '1',
  `PERIODO` varchar(4) NOT NULL DEFAULT '2015',
  PRIMARY KEY (`ID`),
  KEY `ESTATUS` (`ESTATUS`),
  CONSTRAINT `crip_partidas_ibfk_1` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_partidas
-- ----------------------------
INSERT INTO `crip_partidas` VALUES ('2000', 'MATERIALES Y SUMINISTROS', '0', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('2100', 'MATERIALES DE ADMINISTRACIÓN, EMISIÓN DE DOCUMENTOS Y ARTÍCULOS OFICIALES', '2000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('21101', 'MATERIALES Y ÚTILES DE OFICINAS', '2100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('21201', 'MAT. Y ÚT. IMP. Y REPROD.', '2100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('21301', 'MAT. ESTADÍSTICO Y GEOGRÁFICO', '2100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('21401', 'MAT. Y ÚT. PROCES. EN EQ. Y BIEN. INF.', '2100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('21501', 'MATERIAL DE APOYO INFORMATIVO', '2100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('21502', 'MATERIAL PARA INFORMACIÓN EN ACTIV.  DE INVEST. CIENTÍFICA Y TÉCN.', '2100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('21601', 'MATERIAL DE LIMPIEZA', '2100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('2200', 'ALIMENTOS Y UTENSILIOS', '2000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('22103', 'PROD. ALIM. PERSONAL QUE REALIZA LABORES EN CAMPO O SUPERVISIÓN', '2200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('22104', 'PROD. ALIM  PERSONAL EN LAS INSTALACIONES DE LAS DEP. Y ENT.', '2200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('22106', 'PROD. ALIM. PARA EL PERSONAL DERIVADO  ACTIVIDADES EXTRAORDINARIAS', '2200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('22201', 'PRODUCTOS ALIMENTICIOS PARA ANIMALES', '2200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('22301', 'UTENSILIOS PARA EL SERVICIO DE ALIMENTACIÓN', '2200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('2300', 'MATERIAS PRIMAS Y MATERIALES DE PRODUCCIÓN Y COMERCIALIZACIÓN', '2000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('23101', 'PRODUCTOS ALIMENTICIOS AGROPECUARIOS Y FORESTALES ADQUIRIDOS COMO MATERIA PRIMA', '2300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('23201', 'REFACC. ACCES. MENORES DE EDIFICIOS', '2300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('23501', 'PRODUCTOS QUÍMICOS, FARMACÉUTICOS Y DE LABORATORIO ADQUIRIDOS COMO MATERIA PRIMA', '2300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('23701', 'PRODUCTOS DE CUERO, PIEL,PLASTICO Y HULE ADQUIRIDOS COMO MATERIA PRIMA', '2300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('23901', 'OTROS PRODUCTOS ADQUIRIDOS COMO MATERIA PRIMA', '2300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('2400', 'MATERIALES Y ARTÍCULOS DE CONSTRUCCIÓN Y DE REPARACIÓN', '2000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('24101', 'PRODUCTOS MINERALES NO METÁLICOS ', '2400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('24201', 'CEMENTO Y PRODUCTOS DE CONCRETO', '2400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('24401', 'MADERAS Y PRODUCTOS DE MADERA', '2400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('24501', 'VIDRIOS Y PRODUCTOS DE VIDRIO', '2400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('24601', 'MATERIAL ELÉCTRICO Y ELECTRÓNICO', '2400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('24701', 'ARTÍCULOS METÁLICOS PARA LA CONSTRUCCIÓN', '2400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('24801', 'MATS. COMPLEMENTARIOS', '2400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('24901', 'OTROS MATS. T ARTS. PARA LA CONST. Y REPARACION', '2400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('2500', 'PRODUCTOS QUÍMICOS, FARMACÉUTICOS Y DE LABORATORIO', '2000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('25101', 'PRODUCTOS QUÍMICOS BÁSICOS', '2500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('25201', 'PLAGUICIDAS, ABONOS Y FERTILIZANTES', '2500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('25301', 'MEDICINAS Y PRODUCTOS FARMACÉUTICOS', '2500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('25401', 'PRODUCTOS QUÍMICOS, FARMACÉUTICOS Y DE LABORATORIO Y SUMINIST. MÉDICOS', '2500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('25501', 'PRODUCTOS QUÍMICOS, FARMACÉUTICOS Y DE LABORATORIO Y SUMINIST. DE LABORATORIO', '2500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('25601', 'FIBRAS SINTETICAS, HULES, PLASTICOS Y DERIVADOS', '2500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('25901', 'OTROS PRODUCTOS QUÍMICOS', '2500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('2600', 'COMBUSTIBLES, LUBRICANTES Y ADITIVOS', '2000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('26102', 'COMBUSTIBLES, LUBRICANTES Y ADITIVOS PARA VEHÍCULOS TERRESTRES, A. M. L. Y F. DESTINADOS A SERV. PÚBLICOS Y LA OPER. DE PROG. PÚBL.', '2600', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('26103', 'COMBUSTIBLES, LUBRICANTES Y ADITIVOS PARA VEHÍCULOS TERRESTRES, A. M. L. Y F. DESTINADOS A SERV. ADMINISTRATIVOS', '2600', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('2700', 'VESTUARIO BCOS. PRENDAS DE  PROTECCIÓN Y ARTS. DEPORTIVOS', '2000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('27101', 'VESTUARIO Y UNIFORMES', '2700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('27201', 'PRENDAS DE PROTECCIÓN PERSONAL', '2700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('27301', 'ARTÍCULOS DEPORTIVOS', '2700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('27401', 'PRODUCTOS TEXTILES', '2700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('27501', 'BLANCOS Y OTROS PRODUCTOS TEXTILES, EXCEPTO PRENDAS DE VESTIR', '2700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('2900', 'HERRAMIENTAS, REFACCIONES Y ACCESORIOS MENORES', '2000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('29101', 'HERRAMIENTAS MENORES', '2900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('29201', 'REFACCIONES Y ACCESORIOS MENORES DE EDIFICIOS', '2900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('29301', 'REFACCIONES Y ACCESORIOS MENORES MOB EQ. ADMON.', '2900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('29401', 'REFACCIONES Y ACCESORIOS PARA EQUIPO DE CÓMPUTO', '2900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('29501', 'REF. Y ACC. MENORES DE EQUIPO. INST. MÉDICO Y DE LAB.', '2900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('29601', 'REFACCIONES Y ACCESORIOS MENORES DE EQUIPO DE TRANSPORTE', '2900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('29801', 'REFACCIONES Y ACCESORIOS MENORES DE MAQUINARIA Y OTROS EQUIPOS', '2900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('29901', 'REFACCIONES Y ACCESORIOS MENORES OTROS BIENES MUEBLES', '2900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3000', 'SERVICIOS', '0', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3100', 'SERVICIOS BÁSICOS', '3000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('31101', 'SERVICIO ENERGÍA ELÉCTRICA', '3100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('31301', 'SERVICIO DE AGUA', '3100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('31401', 'SERVICIO TELÉFONICO CONVENCIONAL', '3100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('31501', 'SERVICIO TELEFONÍA CELULAR', '3100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('31701', 'SERV. COND. SEÑ. AN. DIG.', '3100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('31801', 'SERVICIO POSTAL', '3100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('31902', 'CONTRATACIÓN DE OTROS SERVICIOS', '3100', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3200', 'SERVICIOS DE ARRENDAMIENTO', '3000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32201', 'ARRENDAMIENTO DE EDIFICIOS Y LOCALES', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32301', 'ARRENDAMIENTO DE EQUIPO Y BIENES INFORMÁTICOS', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32302', 'ARRENDAMIENTO DE MOBILIARIO', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32502', 'ARRENDAMIENTO DE VEHÍCULOS T. A. M. L. Y F. PARA SERVICIOS PÚBL. Y LA OPER. DE PROG. PÚBLICOS', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32503', 'ARRENDAMIENTO DE VEHÍCULOS TERRESTRES, AÉREOS, MARÍTIMOS, LACUSTRES Y FLUVIALES PARA SERVICIOS ADMINISTRATIVOS', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32505', 'ARRENDAMIENTO DE VEHÍCULOS T. A. M. L. Y F. PARA SERVIDORES PÚBLICOS', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32601', 'ARRENDAMIENTO DE MAQUINARIA Y EQUIPO', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32701', 'PATENTES, REGALÍAS Y OTROS', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('32903', 'OTROS ARRENDAMIENTOS', '3200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3300', 'ASESOR. COSULT. INFORM. ESTUD. E INVEST. Y O.', '3000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33104', 'OTRAS ASES. PARA OPERACIÓN DE PROGRAMAS', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33301', 'SERVICIOS DE INFORMÁTICA', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33401', 'SERVICIOS PARA CAPACITACIÓN A SERVIDORES PÚBLICOS', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33501', 'ESTUDIOS E INVESTIGACIONES', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33601', 'SERVICIOS RELACIONADOS CON TRADUCCIONES', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33602', 'OTROS SERVICIOS COMERCIALES', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33603', 'IMPRE. DE DOCTOS. OFI. PARA LA PRESTA. DE SERV. PUBL., IDENTI. FORMATOS ADMItIVOS Y FISCALES, FORMAS VALORADAS, CERTIF. Y TÍTULOS', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33604', 'IMPRE. Y ELAB. DE MAT. INFORMATIVO DERIVADO DE LA OPER. Y ADMON. DE LAS DEPEN. Y ENTIDADES', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33605', 'INFORMACIÓN EN MEDIOS MASIVOS DERIVADA DE LA OPER. Y ADMON. DE LAS DEPENDENCIAS Y ENTIDADES', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33801', 'SERVICIOS DE VIGILANCIA', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33901', 'SUBCONTRATACIÓN DE SERVICIOS CON TERCEROS', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('33903', 'SERVICIOS INTEGRALES', '3300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3400', 'SERVICIOS FINANCIEROS, BANCARIOS Y COMERCIALES', '3000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('34101', 'SERVICIOS BANCARIOS Y FINANCIEROS', '3400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('34501', 'SEGUROS DE BIENES PATRIMONIALES', '3400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('34504', 'VIATICOS NACIONALES P FUNCIONARIOS', '3400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('34601', 'ALMACENAJE, EMBALAJE Y ENVASE', '3400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('34701', 'FLETES Y MANIOBRAS', '3400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3500', 'SERVICIOS DE INSTALACIÓN, REPARACIÓN, MANTENIMIENTO Y CONSERVACIÓN', '3000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35101', 'MANNTO. Y CONSERVACIÓN DE INMUEBLES PARA LA PRESTACIÓN DE SERVICIOS ADMITVOS.', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35102', 'MANTTO. Y CONS. DE INMUEBLES', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35201', 'MANTTO. Y C. MOB. Y EQPO. DE ADMON.', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35301', 'MANTTO. Y CONSER. BIEN. INFORM.', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35401', 'INSTALACIÓN, REPARACIÓN Y  MANTENIMIENTO DE EQUIPO INSTRUMENTAL MÉDICO Y DE LABORATORIO', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35501', 'MANNTO. Y CONSERVACIÓN DE VEHÍCULOS T. A. M. L. Y F.', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35701', 'MANTTO. Y CONS. MAQ. Y EQUIPO', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35801', 'SERV. LAV. LIMP. E HIG.', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('35901', 'SERV. DE JARDINERÍA Y FUMIGACIÓN', '3500', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3700', 'SERVICIOS DE TRASLADO Y VIÁTICOS', '3000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37101', 'PASAJES AÉREOS NACIONALES PARA LABORES EN CAMPO Y DE SUPERVISIÓN', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37104', 'PASAJES AÉREOS NAC. PARA SERVI. PÚBLI. DE MANDO EN EL DESEMPEÑO DE COMISIONES Y FUNCIONES OFIC.', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37106', 'PASAJES AÉREOS INTERNACIONALES PARA SERVIDORES PÚBLICOS EN EL DESEMPEÑO DE COMISIONES Y FUNCIONES OFICIALES', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37201', 'PASAJES TERRESTRES NACIONALES PARA LABORES EN CAMPO Y DE SUPERVISIÓN', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37204', 'PASAJES TERRESTRES NAC. PARA SERVI. PÚBLI. DE MANDO EN EL DESEMPEÑO DE COMISIONES Y FUNCIONES OFIC.', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37206', 'PASAJES TERRESTRES INTERNACIONALES PARA SERVIDORES PÚBLICOS EN EL DESEMPEÑO DE COMISIONES Y FUNCIONES OFICIALES', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37501', 'VIÁTICOS NACIONALES PARA LABORES EN CAMPO Y DE SUPERVISIÓN', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37504', 'VIÁTICOS NACIONALES PARA SERVIDORES PÚBLICOS EN EL DESEMPEÑO DE FUNCIONES OFICIALES', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37602', 'VIÁTICOS EN EL EXTRANJERO PARA SERVIDORES PÚBLICOS EN EL DESEMPEÑO DE COMISIONES Y FUNCIONES OFICIALES', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37701', 'INSTALACIÓN DEL PERSONAL FEDERAL', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('37901', 'GASTOS PARA OPERATIVOS Y TRABAJOS DE CAMPO EN ÁREAS RURALES', '3700', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3800', 'SERVICIOS OFICIALES', '3000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('38301', 'CONGRESOS Y CONVENCIONES', '3800', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('38401', 'EXPOSICIONES', '3800', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('3900', 'OTROS SERVICIOS GENERALES', '3000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('39101', 'FUNERALES Y PAGAS DE DEFUNCIÓN', '3900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('39202', 'OTROS IMPUESTOS Y DERECHOS', '3900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('39401', 'EROGACIONES POR RESOLUCIONES POR AUTORIDAD COMPETENTE', '3900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('39801', 'IMPUESTO SOBRE NÓMINA', '3900', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('4000', 'TRANSFERENCIAS, ASIGNACIONES, SUBSIDIOS Y OTRAS AYUDAS', '0', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('4300', 'SUBSIDIOS Y SUBVENCIONES', '4000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('43101', 'SUBSIDIOS A LA PRODUCCIÓN', '4300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('43902', 'SUBSIDIOS A FIDEICOMISOS PRIVADOS Y ESTATALES', '4300', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('4400', 'AYUDAS SOCIALES', '4000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('44106', 'COMPENSACIONES POR SERVICIOS DE CARÁCTER SOCIAL', '4400', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('5000', 'BIENES MUEBLES, INMUEBELES E INTANGIBLES', '0', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('51101', 'MOBILIARIO. BIEN INSTRUMENTAL', '5000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('53101', 'EQUIPO MÉDICO Y DE LABORATORIO', '5000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('56501', 'EQUIPOS Y APARATOS DE COMUNICACIONES Y TELECOMUNICACIONES', '5000', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('6000', 'INVERSION PUBLICA', '0', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('62201', 'OBRAS DE CONSTRUCCIÓN PARA EDIFICIOS NO HABITACIONALES', '6200', '1', '2015');
INSERT INTO `crip_partidas` VALUES ('62905', 'OTROS SERVICIOS RELACIONADOS CON OBRAS PÚBLICAS', '6200', '1', '2015');

-- ----------------------------
-- Table structure for `crip_permisos`
-- ----------------------------
DROP TABLE IF EXISTS `crip_permisos`;
CREATE TABLE `crip_permisos` (
  `CLV_USUARIO` varchar(25) NOT NULL,
  `PERMISO` varchar(25) NOT NULL,
  `AUT_EXTERNO` varchar(25) NOT NULL,
  `CLV_PROY` varchar(10) NOT NULL,
  `ESTATUS` int(1) NOT NULL,
  PRIMARY KEY (`CLV_USUARIO`),
  KEY `CLV_PROY` (`CLV_PROY`),
  KEY `ESTATUS` (`ESTATUS`),
  KEY `AUT_EXTERNO` (`AUT_EXTERNO`),
  CONSTRAINT `crip_permisos_ibfk_1` FOREIGN KEY (`CLV_USUARIO`) REFERENCES `crip_usuarios` (`USUARIO`),
  CONSTRAINT `crip_permisos_ibfk_3` FOREIGN KEY (`CLV_PROY`) REFERENCES `crip_proy` (`CLV-PROY`),
  CONSTRAINT `crip_permisos_ibfk_4` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`),
  CONSTRAINT `crip_permisos_ibfk_5` FOREIGN KEY (`AUT_EXTERNO`) REFERENCES `crip_proy` (`RESPONSABLE`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_permisos
-- ----------------------------
INSERT INTO `crip_permisos` VALUES ('JLLMEXTI', 'EXT', 'MOPO670124CX7', 'CRIPSC000', '1');

-- ----------------------------
-- Table structure for `crip_proy`
-- ----------------------------
DROP TABLE IF EXISTS `crip_proy`;
CREATE TABLE `crip_proy` (
  `CLV-PROY` varchar(10) NOT NULL DEFAULT '',
  `DESCRIPCION` varchar(150) NOT NULL,
  `RESPONSABLE` varchar(25) NOT NULL,
  `OBJETIVO` varchar(255) NOT NULL,
  `PERIODO` varchar(10) NOT NULL,
  `SEC_EFF` int(3) NOT NULL,
  `ESTATUS` int(1) NOT NULL,
  `FECHA` date NOT NULL,
  `FECH-EFF` date NOT NULL,
  `CLV_DEP` varchar(8) NOT NULL,
  `CLV_AREA` varchar(8) NOT NULL DEFAULT '',
  `RECURSO` double DEFAULT NULL,
  `RESTANTE` double DEFAULT NULL,
  PRIMARY KEY (`CLV-PROY`,`SEC_EFF`,`ESTATUS`,`CLV_DEP`,`CLV_AREA`),
  KEY `CLV_DEP` (`CLV_DEP`),
  KEY `RESPONSABLE` (`RESPONSABLE`) USING BTREE,
  KEY `CLV_PROY` (`CLV-PROY`) USING BTREE,
  KEY `CLV_AREA` (`CLV_AREA`),
  KEY `ESTATUS` (`ESTATUS`),
  CONSTRAINT `crip_proy_ibfk_2` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`),
  CONSTRAINT `crip_proy_ibfk_4` FOREIGN KEY (`CLV_AREA`) REFERENCES `crip_area` (`CLV_AREA`),
  CONSTRAINT `crip_proy_ibfk_5` FOREIGN KEY (`CLV_DEP`) REFERENCES `crip_dependencia` (`CLV_DEP`),
  CONSTRAINT `crip_proy_ibfk_6` FOREIGN KEY (`RESPONSABLE`) REFERENCES `crip_usuarios` (`USUARIO`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=COMPACT;

-- ----------------------------
-- Records of crip_proy
-- ----------------------------
INSERT INTO `crip_proy` VALUES ('CRIPSC000', 'ADMINISTRACION', 'MOPO670124CX7', 'ADMINISTRACION', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC01', '1000000', '100000');
INSERT INTO `crip_proy` VALUES ('CRIPSC001', 'ALTERNATIVAS TECNOLOGICAS PARA EL MEJORAMIENTO OPERATIVO DE LA FLOTA CAMARONERA DEL GOLFO DE TEHUANTEPEC', 'MOPO670124CX7', 'ALTERNATIVAS TECNOLOGICAS PARA EL MEJORAMIENTO OPERATIVO DE LA FLOTA CAMARONERA DEL GOLFO DE TEHUANTEPEC', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC04', '1000000', '100000');
INSERT INTO `crip_proy` VALUES ('CRIPSC002', 'COORDINACION DE LA INVESTIGACION Y ATENCION AL SECTOR', 'MOPO670124CX7', 'COORDINACION DE LA INVESTIGACION Y ATENCION AL SECTOR', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC08', '1000000', '100000');
INSERT INTO `crip_proy` VALUES ('CRIPSC003', 'VALIDACION DEL DESEMPEÑO DE LA TECNOLOGIA DE CULTIVO TENHUAYACA (Petenia splendida) EN LA PRESA MALPASO (NETZAHUALCOYOTL), CHIAPAS', 'MOPO670124CX7', 'VALIDACION DEL DESEMPEÑO DE LA TECNOLOGIA DE CULTIVO TENHUAYACA (Petenia splendida) EN LA PRESA MALPASO (NETZAHUALCOYOTL), CHIAPAS', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC08', '1000000', '100000');
INSERT INTO `crip_proy` VALUES ('CRIPSC004', 'EVALUACION DE LA PESQUERIA ARTESANAL DE TIBURON EN EL GOLFO DE TEHUANTEPEC (OAXACA Y CHIAPAS)', 'MOPO670124CX7', 'EVALUACION DE LA PESQUERIA ARTESANAL DE TIBURON EN EL GOLFO DE TEHUANTEPEC (OAXACA Y CHIAPAS)', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC08', '1000000', '100000');
INSERT INTO `crip_proy` VALUES ('CRIPSC005', 'CARACTERIZACION DE LA PESQUERIA DE ESCAMA EN LA CUENCA MEDIA DEL RIO GRIJALVA, CHIAPAS', 'MOPO670124CX7', 'CARACTERIZACION DE LA PESQUERIA DE ESCAMA EN LA CUENCA MEDIA DEL RIO GRIJALVA, CHIAPAS', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC05', '1000000', '100000');
INSERT INTO `crip_proy` VALUES ('CRIPSC006', 'DETERMINACION DE LA EFICIENCIA DE LAS TRAMPAS EN LA CAPTURA DE LANGOSTAS ESPINOSAS (Panulirus spp) DEL LITORAL COSTERO DEL ESTADO DE OAXACA', 'MOPO670124CX7', 'DETERMINACION DE LA EFICIENCIA DE LAS TRAMPAS EN LA CAPTURA DE LANGOSTAS ESPINOSAS (Panulirus spp) DEL LITORAL COSTERO DEL ESTADO DE OAXACA', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC05', '1000000', '100000');
INSERT INTO `crip_proy` VALUES ('CRIPSC007', 'EVALUACION DE LAS POBLACIONES DE CAMARON Y VARIABILIDAD ECOLOGICA DE LA ICTIOFAUNA ASOCIADA ALA PESQUERIA DE CAMARON EN EL GOLFO DE TEHUANTEPEC, MEXIC', 'MOPO670124CX7', 'EVALUACION DE LAS POBLACIONES DE CAMARON Y VARIABILIDAD ECOLOGICA DE LA ICTIOFAUNA ASOCIADA ALA PESQUERIA DE CAMARON EN EL GOLFO DE TEHUANTEPEC, MEXICO', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC06', '1000000', '100000');
INSERT INTO `crip_proy` VALUES ('CRIPSC008', 'INVESTIGACION BIOLOGICO PESQUERA Y SOCIOECONOMICO DEL DORADO (coryphaena hippurus) EN EL LITORAL DEL PACIFICO MEXICANO (OAXACA-CHIAPAS)', 'MOPO670124CX7', 'INVESTIGACION BIOLOGICO PESQUERA Y SOCIOECONOMICO DEL DORADO (coryphaena hippurus) EN EL LITORAL DEL PACIFICO MEXICANO (OAXACA-CHIAPAS)', '2014', '1', '1', '2014-01-01', '2014-01-01', 'CRIP-SC', 'CRIPSC05', '1000000', '100000');

-- ----------------------------
-- Table structure for `crip_puestos`
-- ----------------------------
DROP TABLE IF EXISTS `crip_puestos`;
CREATE TABLE `crip_puestos` (
  `CLV_NIVEL` varchar(15) NOT NULL,
  `CLV_PUESTO` varchar(100) NOT NULL,
  `CLV_PLAZA` varchar(10) NOT NULL,
  `DESCRIPCION` varchar(50) NOT NULL DEFAULT '',
  `DESC-CORTO` varchar(50) NOT NULL,
  PRIMARY KEY (`CLV_NIVEL`,`CLV_PUESTO`,`CLV_PLAZA`),
  KEY `CLV_PUESTO` (`CLV_PUESTO`),
  KEY `CLV_NIVEL` (`CLV_NIVEL`),
  KEY `CLV_PLAZA` (`CLV_PLAZA`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=COMPACT;

-- ----------------------------
-- Records of crip_puestos
-- ----------------------------
INSERT INTO `crip_puestos` VALUES ('A01807', 'TECNICO DE APOYO EN INVESTIGACION', '0173', 'JEFE DE OFICINA', 'JEFE DE OFICINA');
INSERT INTO `crip_puestos` VALUES ('CFNA001', 'SUBDIRECTOR DE AREA', '0363', 'SUBDIRECTOR DE AREA', 'SUBDIRECTOR DE AREA');
INSERT INTO `crip_puestos` VALUES ('CFPQ2', 'PROFESIONAL DICTAMINADOR DE SERVICIOS ESP.', '0480', 'PROFESIONAL DICTAMINADOR DE SERVICIOS ESP.', 'PROFESIONAL DICTAMINADOR DE SERVICIOS ESP.');
INSERT INTO `crip_puestos` VALUES ('E3420', 'TECNICO AUXILIAR \"C\"', '0065', 'TECNICO AUXILIAR \"C\"', 'TECNICO AUXILIAR \"C\"');
INSERT INTO `crip_puestos` VALUES ('E3421', 'TECNICO ASOCIADO \"A\"', '0434', 'TECNICO ASOCIADO \"A\"', 'TECNICO ASOCIADO \"A\"');
INSERT INTO `crip_puestos` VALUES ('E3423', 'TECNICO ASOCIADO \"C\"', '0269', 'TECNICO ASOCIADO \"C\"', 'TECNICO ASOCIADO \"C\"');
INSERT INTO `crip_puestos` VALUES ('E3423', 'TECNICO DE APOYO EN INVESTIGACION', '9003', 'TECNICO DE APOYO EN INVESTIGACION', 'TECNICO DE APOYO EN INVESTIGACION');
INSERT INTO `crip_puestos` VALUES ('E3423', 'TECNICO DE APOYO EN INVESTIGACION', '9008', 'TECNICO DE APOYO EN INVESTIGACION', 'TECNICO DE APOYO EN INVESTIGACION');
INSERT INTO `crip_puestos` VALUES ('E3423', 'TECNICO DE APOYO EN INVESTIGACION', '9012', 'TECNICO DE APOYO EN INVESTIGACION', 'TECNICO DE APOYO EN INVESTIGACION');
INSERT INTO `crip_puestos` VALUES ('E3429', 'ADJUNTO DE INVESTIGACION', '9058', 'ADJUNTO DE INVESTIGACION', 'ADJUNTO DE INVESTIGACION');
INSERT INTO `crip_puestos` VALUES ('E3429', 'INVESTIGADOR ASOCIADO \"A\"', '0467', 'INVESTIGADOR ASOCIADO \"A\"', 'INVESTIGADOR ASOCIADO \"A\"');
INSERT INTO `crip_puestos` VALUES ('E3429', 'INVESTIGADOR ASOCIADO \"A\"', '0470', 'INVESTIGADOR ASOCIADO \"A\"', 'INVESTIGADOR ASOCIADO \"A\"');
INSERT INTO `crip_puestos` VALUES ('E3433', 'INVESTIGADOR TITULAR \"B\"', '0135', 'INVESTIGADOR TITULAR \"B\"', 'INVESTIGADOR TITULAR \"B\"');
INSERT INTO `crip_puestos` VALUES ('E3434', 'COORDINADOR DE INVESTIGACION', '9075', 'COORDINADOR DE INVESTIGACION', 'COORDINADOR DE INVESTIGACION');
INSERT INTO `crip_puestos` VALUES ('E3434', 'INVESTIGADOR TITULAR \"C\"', '0368', 'INVESTIGADOR TITULAR \"C\"', 'INVESTIGADOR TITULAR \"C\"');
INSERT INTO `crip_puestos` VALUES ('PQ2', 'ENLACE', '0506', 'ENLACE DE ESTADISTICA E INFORMATICA', 'ENLACE DE ESTADISTICA E INFORMATICA');
INSERT INTO `crip_puestos` VALUES ('PQ2', 'ENLACE', '0575', 'ENLACE', 'ENLACE');
INSERT INTO `crip_puestos` VALUES ('T03805', 'TECNICO ESPESPECIALIZADO', '0107', 'TECNICO ESPESPECIALIZADO', 'TECNICO ESPECIALIZADO');

-- ----------------------------
-- Table structure for `crip_roles`
-- ----------------------------
DROP TABLE IF EXISTS `crip_roles`;
CREATE TABLE `crip_roles` (
  `CLV_ROL` varchar(9) NOT NULL DEFAULT '',
  `DESCR` varchar(50) DEFAULT NULL,
  `DESC_LARGA` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`CLV_ROL`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_roles
-- ----------------------------
INSERT INTO `crip_roles` VALUES ('ADMCRIPSC', 'ADMINISTRADOR CRIP SALINACRUZ', 'ROL CREADO PARA EL ADMINISTRADOR DELCRIP SALINA CRUZ');
INSERT INTO `crip_roles` VALUES ('ADMGR', 'ADMINISTRADOR GENERAL', 'ROL CREADO PARA EL DBA - SYSTEM');
INSERT INTO `crip_roles` VALUES ('ADMINP', 'ADMINISTRADOR INAPESCA', 'ROL CREADO PARA ELADMINISTRADOR GRAL DE INAPESCA');
INSERT INTO `crip_roles` VALUES ('INVEST', 'INVESTIGADOR', 'ROL CREADO PARA INVESTIGADORES (USUARIOS SIN PRIVILEGIOS DE ADMINISTRADOR)');
INSERT INTO `crip_roles` VALUES ('JFCCRIPSC', 'JEFE DE CENTRO SALINA CRUZ', 'ROL CREADO PARA JEFE DE CENTRO SALINA CRUZ');

-- ----------------------------
-- Table structure for `crip_secretaria`
-- ----------------------------
DROP TABLE IF EXISTS `crip_secretaria`;
CREATE TABLE `crip_secretaria` (
  `CLV_SECRETARIA` varchar(15) NOT NULL,
  `DESCR` varchar(150) NOT NULL,
  `NOM_CORTO` varchar(150) NOT NULL,
  `FECHA` date NOT NULL,
  `FECH_EFF` date NOT NULL,
  `SECEFF` int(4) NOT NULL,
  `ESTATUS` int(1) NOT NULL,
  PRIMARY KEY (`CLV_SECRETARIA`,`SECEFF`),
  KEY `CLV_SECRETARIA` (`CLV_SECRETARIA`),
  KEY `ESTATUS` (`ESTATUS`),
  CONSTRAINT `crip_secretaria_ibfk_1` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_secretaria
-- ----------------------------
INSERT INTO `crip_secretaria` VALUES ('SAGARPA', 'SECRETARIA DE AGRICULTURA, GANADERIA, DESARROLLO RURAL, PESCA Y ALIMENTACION', 'SECRETARIADE AGRICULTURA, GANADERIA, DESARROLLO RURAL, PESCA Y ALIMENTACION', '2014-11-02', '2014-11-02', '1', '1');

-- ----------------------------
-- Table structure for `crip_transporte`
-- ----------------------------
DROP TABLE IF EXISTS `crip_transporte`;
CREATE TABLE `crip_transporte` (
  `CLV_TRANSPORTE` varchar(4) NOT NULL,
  `CLV_CLASE` varchar(3) NOT NULL,
  `DESC_CLASE` varchar(150) NOT NULL,
  `TIPO` varchar(2) NOT NULL,
  `DESC_TIPO` varchar(50) NOT NULL,
  `N_ECO` int(2) DEFAULT NULL,
  `DESCRIPCION` varchar(100) DEFAULT NULL,
  `MARCA` varchar(25) DEFAULT NULL,
  `MODELO` int(4) DEFAULT NULL,
  `PLACAS` varchar(20) DEFAULT NULL,
  `ESTATUS` int(1) NOT NULL,
  `OBSERVACIONES` varchar(150) DEFAULT NULL,
  `CLV_DEP` varchar(8) NOT NULL,
  PRIMARY KEY (`CLV_TRANSPORTE`),
  KEY `CLV_TRANSPORTE` (`CLV_TRANSPORTE`),
  KEY `CLV_CLASE` (`CLV_CLASE`),
  KEY `TIPO` (`TIPO`),
  KEY `N_ECO` (`N_ECO`),
  KEY `ESTATUS` (`ESTATUS`),
  KEY `CLV_DEP` (`CLV_DEP`),
  CONSTRAINT `crip_transporte_ibfk_2` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`),
  CONSTRAINT `crip_transporte_ibfk_3` FOREIGN KEY (`CLV_DEP`) REFERENCES `crip_dependencia` (`CLV_DEP`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_transporte
-- ----------------------------
INSERT INTO `crip_transporte` VALUES ('T001', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '1', 'HILUX DOBLE CABINA COLOR BLANCA', 'TOYOTA', '2013', 'MPP6522', '1', 'NUEVO ARRENDADO', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T002', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '2', 'HILUX DOBLE CABINA COLOR BLANCA', 'TOYOTA', '2013', 'MPP6545', '1', 'NUEVO ARRENDADO', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T003', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '3', 'TIIDA SEDAN SENSE T/M 1.8 COLOR BLANCO', 'NISSAN', '2014', 'MPP6543', '1', 'NUEVO ARRENDADO', 'CRIP-MZ');
INSERT INTO `crip_transporte` VALUES ('T004', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '4', 'CAMIONETA  DOBLE CABINA COLOR BLANCA', 'NISSAN', '2010', 'RV62380', '1', 'BUEN ESTADO', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T005', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '5', 'SENTRA GST COLOR BLANCO', 'NISSAN', '2000', '770PXN', '0', 'EN REPARACION', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T006', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '6', 'RANGER COLOR BLANCO', 'FORD', null, 'MNU3373', '1', 'BUEN ESTADO', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T007', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '7', 'AVEO  COLOR BLANCO', 'CHEVROLET', null, 'MSD1655', '1', 'BUEN ESTADO', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T008', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '8', 'CAMIONETA PICK UP COLOR BLANCA', 'CHEVROLET', '1998', 'RU94662', '1', 'VEHICULO EN ACAPULCO RESGUARDADO POR ESTABAN CABRERA MANCILLA', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T009', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '9', 'SENTRA SEDAN GSS COLOR GUINDA', 'NISSAN', '1997', '787MSX', '4', 'EN TRAMITE DE BAJA', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T010', 'TER', 'TERRESTRE', 'VO', 'VEHICULO OFICIAL', '10', 'CAMIONETA  DOBLE CABINA COLOR AZUL', 'NISSAN', '2004', 'RU94592', '4', 'EN TRAMITE DE BAJA', 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T011', 'TER', 'TERRESTRE', 'AP', 'AUTO PERSONAL', null, null, null, null, null, '1', null, 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T012', 'TER', 'TERRESTRE', 'AB', 'AUTOBUS', null, null, null, null, null, '1', null, 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T013', 'AER', 'AEREO', 'AE', 'AVION', null, null, null, null, null, '1', null, 'CRIP-SC');
INSERT INTO `crip_transporte` VALUES ('T014', 'ACU', 'ACUATICO', 'AC', 'ACUATICO', null, null, null, null, null, '1', null, 'CRIP-SC');

-- ----------------------------
-- Table structure for `crip_usuarios`
-- ----------------------------
DROP TABLE IF EXISTS `crip_usuarios`;
CREATE TABLE `crip_usuarios` (
  `USUARIO` varchar(25) NOT NULL,
  `NOMBRE` varchar(50) DEFAULT NULL,
  `AP_PAT` varchar(50) DEFAULT NULL,
  `AP_MAT` varchar(50) DEFAULT NULL,
  `FECH_NAC` date NOT NULL,
  `CALLE` varchar(50) DEFAULT NULL,
  `NUM-EXT` int(4) DEFAULT NULL,
  `NUM_INT` varchar(4) DEFAULT NULL,
  `COLONIA` varchar(30) DEFAULT NULL,
  `DELEGACION` varchar(30) DEFAULT NULL,
  `CD` varchar(5) NOT NULL,
  `CLV_ESTADO` varchar(3) NOT NULL,
  `CLV_PAIS` varchar(3) NOT NULL,
  `RFC` varchar(15) NOT NULL,
  `CURP` varchar(20) NOT NULL,
  `EMAIL` varchar(100) DEFAULT NULL,
  `FECHA` date NOT NULL,
  `FECHEFF` date DEFAULT NULL,
  `ESTATUS` int(1) DEFAULT NULL,
  `SEC_EFF` int(4) DEFAULT NULL,
  PRIMARY KEY (`USUARIO`,`RFC`,`CURP`),
  KEY `USUARIO` (`USUARIO`),
  KEY `CLV_PAIS` (`CLV_PAIS`),
  KEY `CLV_ESTADO` (`CLV_ESTADO`),
  KEY `CD` (`CD`),
  KEY `ESTATUS` (`ESTATUS`),
  CONSTRAINT `crip_usuarios_ibfk_1` FOREIGN KEY (`CLV_PAIS`) REFERENCES `crip_pais` (`CLV_PAIS`),
  CONSTRAINT `crip_usuarios_ibfk_2` FOREIGN KEY (`CLV_ESTADO`) REFERENCES `crip_estado` (`CLV_ESTADO`),
  CONSTRAINT `crip_usuarios_ibfk_3` FOREIGN KEY (`CD`) REFERENCES `crip_ciudad` (`CLV_CIUDAD`),
  CONSTRAINT `crip_usuarios_ibfk_4` FOREIGN KEY (`ESTATUS`) REFERENCES `crip_estatus` (`CLV_ESTATUS`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of crip_usuarios
-- ----------------------------
INSERT INTO `crip_usuarios` VALUES ('AAAA890210TD0', 'ADRIANA JAZMIN', 'ALATORRE', 'ALBA', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'AAAA890210TD0', 'AAAA890210MNLLD04', 'alatorre.inapesca@gmail.com ; adryjaz_10@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('BEAA680909RT7', 'ANA LILIA ', 'BETANZOS', 'AVENDAÑO', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'BEAA680909RT7', 'BEAA680909MOCTVN06', 'alilia_beta@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('COGX7409176P8', 'ISABEL ', 'CORTES', 'GONZALEZ', '2014-12-08', null, null, null, null, null, 'SC', '20', 'MEX', 'COGX7409176P8', 'CXGI740917MOCRNS06', 'chabely1975@hotmail.com', '2014-12-08', '2014-12-08', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('FATN800730AD7', 'NIDIA', 'FARRERA', 'TOLEDO', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'FATN800730AD7', 'FATN800730MOCRLD00', 'aidin48@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('HECJ861110BV0', 'JESUS LEONARDO', 'HERNANDEZ', 'CORONA', '2014-12-08', null, null, null, null, null, 'SC', '20', 'MEX', 'HECJ861110BV0', 'HECJ861110HGTRRS09', 'jesleo_38@hotmail.com', '2014-12-08', '2014-12-08', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('JLLMEXTI', 'JUAN ANTONIO', 'LÓPEZ', 'LÓPEZ', '1987-06-16', 'ARISTA', '18', null, 'BO. LABORIO', 'CENTRO', 'TH', '20', 'MEX', 'LOLJ870616C25', 'LOLJ870616HOCPPN06', 'LOPEZ.JA@LIVE.COM.MX', '2014-01-01', '2014-01-01', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('LACA831007AT1', 'ALDRIN ', 'LABASTIDA', 'CHE', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'LACA831007AT1', 'LACA831007HOCBHL05', 'aldrinlc@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('LIRA890424R61', 'ANA ALEJANDRA', 'LIZARRAGA', 'RODRIGUEZ', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'LIRA890424R61', 'LIRA890424MJCZDN07', 'shanen182@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('MARE790321GI6', 'ELY NAW', 'MACIAS', 'RAMIREZ', '2014-12-08', null, null, null, null, null, 'SC', '20', 'MEX', 'MARE790321GI6', 'MARE790321HMNCML00', 'elynawmacias@gmail.com', '2014-12-08', '2014-12-08', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('MOPO670124CX7', 'OSWALDO ', 'MORALES', 'PACHECO', '2008-12-01', null, null, null, null, null, 'SC', '20', 'MEX', 'MOPO670124CX7', 'MOPO670124HSRRCS04', 'oswaldmora@yahoo.com.mx', '2014-02-04', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('NUOA7803309D6', 'ADA LISBETH', 'NUÑEZ', 'OROZCO', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'NUOA7803309D6', 'NUOA780330MOCXRD00', 'adalis_78@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('OIPA810504FX0', 'JOSÉ ALFONSO ', 'OVIEDO', 'PIAMONTE', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'OIPA810504FX0', 'OIPA810504HOCVML08', 'jaoviedo_7@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('PIGC770412E59', 'CUDBERTO', 'PINEDA', 'GARCIA', '2014-12-08', null, null, null, null, null, 'SC', '20', 'MEX', 'PIGC770412E59', 'PIGC770412HOCNRD00', 'cudber@hotmail.com', '2014-12-08', '2014-12-08', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('RASE750419CQ9', 'EDUARDO', 'RAMOS', 'SANTIAGO', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'RASE750419CQ9', 'RASE750419HOCMND00', 'edurasa@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('SAMJ8505157HA', 'JONATHAN', 'SANCHEZ', 'MALDONADO', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'SAMJ8505157HA', 'SAMJ850515HOCNLN03', 'yonysamy@hotmail.com', '0000-00-00', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('VEML590903D66', 'JOSE LUIS', 'VEGA', 'MENDOZA', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'VEML590903D66', 'VEML590903HOCGNS04', null, '2014-12-05', '2014-12-05', '1', '1');
INSERT INTO `crip_usuarios` VALUES ('VITJ810405CI5', 'JESUS', 'VILLALOBOS ', 'TOLEDO', '2014-12-05', null, null, null, null, null, 'SC', '20', 'MEX', 'VITJ810405CI5', 'VITJ810405HOCLLS00', 'villatoledo05@yahoo.com.mx;tiburoperro@hotmail.com', '2014-12-05', '2014-12-05', '1', '1');

-- ----------------------------
-- View structure for `vw_localidades`
-- ----------------------------
DROP VIEW IF EXISTS `vw_localidades`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_localidades` AS select `a`.`CLV_PAIS` AS `PAIS_ID`,`a`.`NOM_CORTO` AS `PAIS`,`b`.`CLV_ESTADO` AS `ESTADO_ID`,`b`.`NOM_CORTO` AS `ESTADO`,`c`.`CLV_CIUDAD` AS `CIUDAD_ID`,`c`.`NOM_CORTO` AS `CIUDAD` from ((`crip_pais` `a` join `crip_estado` `b`) join `crip_ciudad` `c`) where ((`b`.`CLV_PAIS` = `a`.`CLV_PAIS`) and (`b`.`CLV_ESTADO` = `c`.`CLV_ESTADO`));

-- ----------------------------
-- View structure for `vw_scheduller`
-- ----------------------------
DROP VIEW IF EXISTS `vw_scheduller`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_scheduller` AS select distinct `b`.`CVL_OFICIO` AS `CLV_OFICIO`,`b`.`FOLIO` AS `FOLIO`,`a`.`CLV_TRANSPORTE` AS `TRANSPORTE_SOL`,`a`.`CLV_CLASE` AS `CLV_CLASE`,`a`.`TIPO` AS `TIPO_TRANSP`,`a`.`N_ECO` AS `N_ECO`,`a`.`CLV_DEP` AS `DEPENDENCIA`,`b`.`FECHA_SOL` AS `FECHA_SOLICITUD`,`b`.`FECHA_I` AS `FECHA_I`,`b`.`FECHA_F` AS `FECHA_F`,`b`.`USSER` AS `USUARIO`,`b`.`CLV_TRANS_AUT` AS `TRANSPORTE_AUT` from (`crip_transporte` `a` join `crip_comision` `b`) where ((`a`.`CLV_TRANSPORTE` = `b`.`CLV_TRANS-SOL`) and (`a`.`CLV_CLASE` = `b`.`CLV_CLASE`) and (`a`.`TIPO` = `b`.`CLV_TIPO_TRANS`) and (`a`.`ESTATUS` = 1) and (`b`.`ESTATUS` = 1));

-- ----------------------------
-- View structure for `vw_usuarios`
-- ----------------------------
DROP VIEW IF EXISTS `vw_usuarios`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vw_usuarios` AS select distinct `a`.`USUARIO` AS `USSER`,`a`.`PASSWORD` AS `PASSWORD`,`b`.`NOMBRE` AS `NOMBRE`,`b`.`AP_PAT` AS `APELLIDO_PAT`,`b`.`AP_MAT` AS `APELLIDO_MAT`,`b`.`RFC` AS `RFC`,`a`.`EMAIL_INST` AS `EMAIL`,`a`.`CARGO` AS `CARGO`,`a`.`CLV_NIVEL` AS `NIVEL`,`a`.`CLV_PLAZA` AS `PLAZA`,`a`.`CLV_PUESTO` AS `PUESTO`,`a`.`CLV_SECRE` AS `SECRETARIA`,`a`.`CLV_ORG` AS `ORGANISMO`,`a`.`CLV_DEP` AS `DEPENDENCIA`,`a`.`CLV_AREA` AS `AREA`,`a`.`CLV_PROY` AS `PROY`,`a`.`ROL` AS `ROL` from (`crip_job` `a` join `crip_usuarios` `b` on(('' = ''))) where ((`a`.`USUARIO` = `b`.`USUARIO`) and (`a`.`ESTATUS` = `b`.`ESTATUS`) and (`b`.`ESTATUS` = 1));
