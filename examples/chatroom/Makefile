SERVER_OUT := "server.bin
GO_API_OUT := "chatroom/chatroom.pb.go"
PKG := "github.com/qapquiz/cheirokmeta"

.PHONY: api clean help

api: chatroom/chatroom.pb.go ## Auto-generate grpc go sources

chatroom/chatroom.pb.go: chatroom/chatroom.proto
	@protoc -I chatroom/ \
					-I${GOPATH}/src \
					--go_out=plugings=grpc:chatroom \
					chatroom/chatroom.proto

clean: ## Remove previous builds
	@rm $(GO_API_OUT)

help: ## Display this help screen
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'