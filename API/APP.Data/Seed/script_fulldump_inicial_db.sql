-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: todoapp
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Todos`
--
DROP DATABASE IF EXISTS todoapp;

CREATE DATABASE todoapp;

use todoapp;

DROP TABLE IF EXISTS `Todos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Todos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Created` datetime(6) NOT NULL,
  `Title` varchar(100) NOT NULL,
  `DateDeleted` datetime(6) DEFAULT NULL,
  `DeletedByUserId` int DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0',
  `IsDone` tinyint(1) NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Todos_DeletedByUserId` (`DeletedByUserId`),
  CONSTRAINT `ToDo.Possui.UserDeleted` FOREIGN KEY (`DeletedByUserId`) REFERENCES `Users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Todos`
--

LOCK TABLES `Todos` WRITE;
/*!40000 ALTER TABLE `Todos` DISABLE KEYS */;
/*!40000 ALTER TABLE `Todos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDeleted` tinyint(1) NOT NULL,
  `DateDeleted` datetime(6) DEFAULT NULL,
  `DeletedByUserId` int DEFAULT NULL,
  `Username` varchar(100) NOT NULL,
  `PasswordHash` varchar(1000) NOT NULL,
  `PasswordSalt` varchar(1000) NOT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Users_DeletedByUserId` (`DeletedByUserId`),
  CONSTRAINT `User.Possui.UserDeleted` FOREIGN KEY (`DeletedByUserId`) REFERENCES `Users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'2020-02-28 11:16:06',0,NULL,NULL,'admin','bxkiAcxoWZStu02HuRkjxQ+mXvSFEq8pepzES/XM2Znd930uMWzM/VVuYBmlYgDwzfsPl+KVovd2jZLsdvAODg==','CtIedKHIfNOrQe50bBevyDH6rj1BQ25icP3jjTx7W0Bxjg213ZJpBp1xvdgddxmSc2mLiQlpGhH9bfXW6WPYuQxaY91wajNtKvggpy79AcqzfOXeMjWc/VxvJDlzRZeMZHEU/yqR1ZUoQQddLtxR07FwjrbLS848Axxm8kWtM2U=','email@gmail.com');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20200221173900_initial','3.1.2'),('20200221174711_UpdateVarcharLimitUser','3.1.2'),('20200226172004_AdicionaRefDeletedUserTodoEntity','3.1.2');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-02-28 11:16:42