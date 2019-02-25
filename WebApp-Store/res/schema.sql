create table if not exists users (
  id integer primary key autoincrement,
  username string not null,
  email_address string not null,
  password string not null
);