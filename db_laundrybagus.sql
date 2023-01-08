-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 08 Jan 2023 pada 07.15
-- Versi server: 10.4.24-MariaDB
-- Versi PHP: 7.4.29

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_laundrybagus`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_customer`
--

CREATE TABLE `tb_customer` (
  `ID_Customer` varchar(7) NOT NULL,
  `Nama` varchar(50) NOT NULL,
  `Jenis_Kelamin` varchar(15) NOT NULL,
  `Alamat` varchar(50) NOT NULL,
  `No_HP` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_customer`
--

INSERT INTO `tb_customer` (`ID_Customer`, `Nama`, `Jenis_Kelamin`, `Alamat`, `No_HP`) VALUES
('CUS0001', 'Veno', 'Laki - Laki', 'Cikarang Utara', '087747935298'),
('CUS0002', 'Maulana', 'Laki - Laki', 'Cikarang', '0877'),
('CUS0003', 'Krishna', 'Laki - Laki', 'Tambun', '0877'),
('CUS0004', 'Rofi', 'Laki - Laki', 'Cikarang', '0877123'),
('CUS0005', 'Riska', 'Perempuan', 'Cikarang', '087886866941');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_mesincuci`
--

CREATE TABLE `tb_mesincuci` (
  `ID_MesinCuci` varchar(15) NOT NULL,
  `Jumlah_MesinCuci` varchar(3) NOT NULL,
  `HargaPerMesinCuci` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_mesincuci`
--

INSERT INTO `tb_mesincuci` (`ID_MesinCuci`, `Jumlah_MesinCuci`, `HargaPerMesinCuci`) VALUES
('2211260002', '3', '10000'),
('2211260005', '2', '10000'),
('2211260006', '5', '10000'),
('2211260007', '3', '10000'),
('2211260008', '1', '10000'),
('2212090009', '4', '10000');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tb_transaksi`
--

CREATE TABLE `tb_transaksi` (
  `Faktur` varchar(15) NOT NULL,
  `Tanggal_Masuk` datetime NOT NULL,
  `Tanggal_Keluar` datetime DEFAULT NULL,
  `ID_Customer` varchar(7) NOT NULL,
  `ID_MesinCuci` varchar(15) NOT NULL,
  `Harga` varchar(20) NOT NULL,
  `DP` varchar(20) NOT NULL,
  `Total_Harga` varchar(20) NOT NULL,
  `Keterangan` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tb_transaksi`
--

INSERT INTO `tb_transaksi` (`Faktur`, `Tanggal_Masuk`, `Tanggal_Keluar`, `ID_Customer`, `ID_MesinCuci`, `Harga`, `DP`, `Total_Harga`, `Keterangan`) VALUES
('2211260002', '2022-11-26 11:05:21', '2022-11-26 11:05:29', 'CUS0002', '2211260002', '30000', '0', '30000', 'Sudah Lunas'),
('2211260005', '2022-11-26 11:58:40', NULL, 'CUS0003', '2211260005', '20000', '0', '20000', 'Belum Lunas'),
('2211260006', '2022-11-26 12:07:15', NULL, 'CUS0001', '2211260006', '50000', '0', '50000', 'Belum Lunas'),
('2211260007', '2022-11-26 12:07:49', NULL, 'CUS0001', '2211260007', '30000', '0', '30000', 'Belum Lunas'),
('2211260008', '2022-11-26 12:09:08', NULL, 'CUS0005', '2211260008', '10000', '0', '10000', 'Belum Lunas'),
('2212090009', '2022-12-09 18:31:58', '2022-12-09 18:32:12', 'CUS0003', '2212090009', '40000', '0', '40000', 'Sudah Lunas');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `tb_customer`
--
ALTER TABLE `tb_customer`
  ADD PRIMARY KEY (`ID_Customer`);

--
-- Indeks untuk tabel `tb_mesincuci`
--
ALTER TABLE `tb_mesincuci`
  ADD PRIMARY KEY (`ID_MesinCuci`);

--
-- Indeks untuk tabel `tb_transaksi`
--
ALTER TABLE `tb_transaksi`
  ADD PRIMARY KEY (`Faktur`),
  ADD KEY `ID_Customer` (`ID_Customer`),
  ADD KEY `ID_MesinCuci` (`ID_MesinCuci`);

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `tb_mesincuci`
--
ALTER TABLE `tb_mesincuci`
  ADD CONSTRAINT `tb_mesincuci_ibfk_1` FOREIGN KEY (`ID_MesinCuci`) REFERENCES `tb_transaksi` (`ID_MesinCuci`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ketidakleluasaan untuk tabel `tb_transaksi`
--
ALTER TABLE `tb_transaksi`
  ADD CONSTRAINT `tb_transaksi_ibfk_1` FOREIGN KEY (`ID_Customer`) REFERENCES `tb_customer` (`ID_Customer`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
