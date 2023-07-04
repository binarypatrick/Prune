GITHUB_LATEST_VERSION=$(curl -L -s -H 'Accept: application/json' https://github.com/binarypatrick/prune/releases/latest | sed -e 's/.*"tag_name":"\([^"]*\)".*/\1/')
GITHUB_FILE="prune-linux-x64.tar.gz"
GITHUB_URL="https://github.com/BinaryPatrick/Prune/releases/download/${GITHUB_LATEST_VERSION}/${GITHUB_FILE}"

curl -s -L -o prune-linux-x64.tar.gz $GITHUB_URL
tar xzf prune-linux-x64.tar.gz ./prune
install -Dm 755 prune -t /usr/local/bin
rm prune prune-linux-x64.tar.gz