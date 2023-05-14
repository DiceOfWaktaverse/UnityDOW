BUILD_RESULT=$1
BUILD_NUMBER=$2
ELAPSED_MILLISECONDS=$3

# Discord Webhook URL
WEBHOOK_URL="https://discord.com/api/webhooks/1106113176866656326/e1k3lcO4sOfCjwKcwxslMrgcEJSBKImOsR41eI7gDAfX4pLm3bjQA6BI23e04jDqUsq7"
# WEBHOOK_URL = 'https://discord.com/api/webhooks/1107182183392747550/szIdxAUbBO2kloeqC5eiEi5pB6PY0K7PqKUd6rnCs8tqUaV00dwQOIiqEBxeN2chkzsA'

# Server config
BUILD_SERVER_URL="https://build.kickpunch.xyz"
BUILD_PIPELINE_NAME="Dice%20of%20Waktaverse%20Test%20Build"
BUILD_ARTEFACT_URL="$BUILD_SERVER_URL/job/$BUILD_PIPELINE_NAME/$BUILD_NUMBER/artifact/build.zip"
BUILD_CONSOLE_URL="$BUILD_SERVER_URL/job/$BUILD_PIPELINE_NAME/$BUILD_NUMBER/console"

# list of random string
USERNAME_LIST=("💖빌드시💖" "💩내가싼 빌드💩" "🍔빌드는 모시깽이🍔" "😲형 빌드 봤어?😲")

# random pick username
USERNAME=${USERNAME_LIST[$RANDOM % ${#USERNAME_LIST[@]}]}

# currenty timestamp as YYYY-MM-DD HH:MM:SS formatted with +9 timezone
TIMESTAMP=$(TZ=Asia/Seoul date "+%Y-%m-%d %H:%M:%S")

# build elapsed time in m분 s초 format
ELAPSED=$(printf "%d분 %d초" $(($ELAPSED_MILLISECONDS/1000/60)) $(($ELAPSED_MILLISECONDS/1000%60)))

# Color
GREEN=5763719
RED=15548997

generate_success_data()
{
  cat <<EOF
{
  "username": "$USERNAME",
  "embeds": [
    {
      "color": $GREEN,
      "description": "빌드가 성공하였습니다 - [다운로드]($BUILD_ARTEFACT_URL)",
      "footer": {
        "text": "- 종료시점: $TIMESTAMP\n- 소요시간: $ELAPSED"
      }
    }
  ]
}
EOF
}

generate_failure_data()
{
  cat <<EOF
{
  "username": "$USERNAME",
  "embeds": [
    {
      "color": $RED,
      "description": "빌드가 실패하였습니다 - [로그보기]($BUILD_CONSOLE_URL)",
      "footer": {
        "text": "- 종료시점: $TIMESTAMP\n- 소요시간: $ELAPSED"
      }
    }
  ]
}
EOF
}

# send notification by curl
if [ "$BUILD_RESULT" = "SUCCESS" ]; then
  curl -X POST "$WEBHOOK_URL" -H "Content-Type: application/json" -d "$(generate_success_data)"
else
  curl -X POST "$WEBHOOK_URL" -H "Content-Type: application/json" -d "$(generate_failure_data)"
fi