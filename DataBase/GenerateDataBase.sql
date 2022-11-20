-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema library_app_db
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema library_app_db
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `library_app_db` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `library_app_db` ;

-- -----------------------------------------------------
-- Table `library_app_db`.`booktb`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `library_app_db`.`booktb` (
  `IdBook` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `Genre` VARCHAR(45) NULL DEFAULT NULL,
  `Author` VARCHAR(45) NOT NULL,
  `WrittenDate` DATETIME NULL DEFAULT NULL,
  `BarCode` VARCHAR(45) NOT NULL,
  `AvailableQuantity` INT NULL DEFAULT NULL,
  `RentedQuantity` INT NULL DEFAULT NULL,
  PRIMARY KEY (`IdBook`),
  UNIQUE INDEX `idBookTb_UNIQUE` (`IdBook` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `library_app_db`.`persontb`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `library_app_db`.`persontb` (
  `CPF` BIGINT NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Birth` DATE NULL DEFAULT NULL,
  `Email` VARCHAR(45) NULL DEFAULT NULL,
  `Phone` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`CPF`),
  UNIQUE INDEX `IdPerson_UNIQUE` (`CPF` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `library_app_db`.`renttb`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `library_app_db`.`renttb` (
  `IdRent` INT NOT NULL AUTO_INCREMENT,
  `IdBook` INT NOT NULL,
  `CPF` BIGINT NOT NULL,
  `RentedDate` DATETIME NOT NULL,
  `ReturnDate` DATETIME NOT NULL,
  `Status` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`IdRent`),
  INDEX `IdBook_idx` (`IdBook` ASC) VISIBLE,
  INDEX `_idx` (`CPF` ASC) VISIBLE,
  CONSTRAINT `CPF`
    FOREIGN KEY (`CPF`)
    REFERENCES `library_app_db`.`persontb` (`CPF`),
  CONSTRAINT `IdBook`
    FOREIGN KEY (`IdBook`)
    REFERENCES `library_app_db`.`booktb` (`IdBook`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `library_app_db`.`usertb`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `library_app_db`.`usertb` (
  `IdUser` INT NOT NULL AUTO_INCREMENT,
  `Email` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`IdUser`),
  UNIQUE INDEX `IdUser_UNIQUE` (`IdUser` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
