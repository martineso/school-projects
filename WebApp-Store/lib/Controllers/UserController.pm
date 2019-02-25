use Crypt::SaltedHash;
use Data::Dumper;
use Dancer2::Plugin::Database;
require "$FindBin::Bin/../lib/Controllers/DbController.pm";

# ---
# provides convinience methods for creating 
# fetching and updating users in the db
# ---

# returns a crypt object for creating or validating hashed passwords
my $csh = Crypt::SaltedHash->new(algorithm => 'SHA-1');

# returns a hash if the user exists, otherwise returns undef
sub fetch_user {
    my $username = shift;
    my $user = database->quick_select('users', { username => $username});
    return $user;
}

sub validate_password {
    my ($hashed_pass, $pass) = @_;

    return $csh->validate($hashed_pass, $pass);
}

sub insert_user_into_db {
    my %user = %{shift()};

    # Hash the password
    my $pass = $user{password};
    $csh->add($pass);
    $pass = $csh->generate;
    $user{password} = $pass;

    database->quick_insert('users', \%user);
}

sub is_employee {
    my $user_id = shift;

    my $emp = database->quick_select('employees', { user_id => $user_id });

    if($emp) {
        return true;
    } else {
        return false;
    }
}

true;