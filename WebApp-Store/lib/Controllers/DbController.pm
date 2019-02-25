use Dancer2;
use Dancer2::Plugin::Database;

hook 'database_connected' => sub {
    debug("database connected!");
};

hook 'database_connection_failed' => sub {
    debug("connection failed!");
};

hook 'database_error' =>sub {
     debug("connection failed!");
};

true;
