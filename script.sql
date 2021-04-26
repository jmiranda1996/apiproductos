create database apiproductos
use apiproductos
create table products (
	id				int					not null,
	name			varchar(200)		not null,
	type			varchar(50)			not null,
	price			float				not null
	primary key (id)
)