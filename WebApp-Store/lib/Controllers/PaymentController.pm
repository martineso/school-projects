use Dancer2;
use FindBin;
use Digest::HMAC_SHA1 'hmac_sha1_hex';
require "$FindBin::Bin/../lib/Controllers/CartController.pm";
use Data::UUID;
use Data::Dumper;

# CONSTANTS
# probably not the best place to store these credentials
use constant API_BASE_WEB => 'https://demo.epay.bg/xdev/mobile';
use constant API_NOREG_ROUTE => '/api/payment/noreg/send';
use constant API_GET_TOKEN_ROUTE => '/api/start';
use constant APPID => '8859111303056402405873530435769803642637529865867040044875408506';
use constant APPSECRET => 'Iq05EcfZ0A3O2FT8jJeDIPaWbMb3VtlVYizoeyjUCHdqpyZ18PfB7h0YDrJcJHbcKiCuj3yXISONepcGSZ0tLrfWfW7QaGcztiL5koD9LmdmATksaGMWHy7dMN6PDv2P';
use constant RCPT => '5048172152';
use constant RCPT_TYPE => 'KIN';


sub get_noreg_payment_url {
    
    my $DESCRIPTION = shift;
    my $REASON = shift;

    my %params = (
        'APPID' => APPID,
        'RCPT' => RCPT,
        'RCPT_TYPE' => RCPT_TYPE,
        'DEVICEID' => get_device_id(), 
        'ID' => get_unique_key(),
        'AMOUNT' => get_subtotal_cents(),
        'DESCRIPTION' => $DESCRIPTION,
        'REASON' => $REASON
    );

    my $CHECKSUM = get_checksum(\%params);
    debug($CHECKSUM);

    my $url = join("", API_BASE_WEB, API_NOREG_ROUTE);
    $url = $url . "?APPID=" . $params{'APPID'} . "&DEVICEID=" . $params{'DEVICEID'} 
                . "&ID=" . $params{'ID'} . "&AMOUNT=" . $params{'AMOUNT'} . "&RCPT=" . $params{'RCPT'} 
                . "&RCPT_TYPE=" . $params{'RCPT_TYPE'} . "&DESCRIPTION=" . $params{'DESCRIPTION'} 
                . "&REASON=" . $params{'REASON'} . "&CHECKSUM=" . $CHECKSUM;
    return $url;    
}

sub get_checksum {
    my %params = %{shift()};
    my @keys = sort(keys %params);

    my $checksum = "";
    for my $key (@keys) {
        $checksum = $checksum . $key . $params{$key} . "\n";
    }
    debug("CHECKSUM\n");
    debug(Dumper($checksum));
    return hmac_sha1_hex($checksum, APPSECRET);
}

sub get_token_url { 
    my $DEVICE_ID = get_device_id(); 
    my $KEY = get_unique_key();

    my $token_url = join('', API_BASE_WEB, API_GET_TOKEN_ROUTE);
    $token_url = $token_url . '?APPID=' . APPID . '&DEVICEID=' . $DEVICE_ID . '&KEY=' . $KEY;
    return $token_url;
}

sub get_subtotal_cents {
    my $subtotal_cents = 100 * get_subtotal();
    return $subtotal_cents;
}

sub get_device_id {

    my $device_id = session("device_id");

    my $gen = Data::UUID->new();
    my $uuid = $gen->create_str;

    if($device_id) {
        return $device_id;
    } else {
        session "device_id" => $uuid;
        return $uuid;
    }
}

sub get_unique_key {
    my $key = session("unique_key");
    my $gen = Data::UUID->new();
    my $uuid = $gen->create_str;

    if($key) {
        return $key;
    } else {
        session "unique_key" => $uuid;
        return $uuid;
    }
}

true;
