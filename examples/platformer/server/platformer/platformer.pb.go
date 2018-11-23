// Code generated by protoc-gen-go. DO NOT EDIT.
// source: platformer.proto

package platformer

import (
	context "context"
	fmt "fmt"
	proto "github.com/golang/protobuf/proto"
	grpc "google.golang.org/grpc"
	math "math"
)

// Reference imports to suppress errors if they are not otherwise used.
var _ = proto.Marshal
var _ = fmt.Errorf
var _ = math.Inf

// This is a compile-time assertion to ensure that this generated file
// is compatible with the proto package it is being compiled against.
// A compilation error at this line likely means your copy of the
// proto package needs to be updated.
const _ = proto.ProtoPackageIsVersion2 // please upgrade the proto package

type PlayerData struct {
	Id                   int32           `protobuf:"varint,1,opt,name=id,proto3" json:"id,omitempty"`
	Name                 string          `protobuf:"bytes,2,opt,name=name,proto3" json:"name,omitempty"`
	Position             *PlayerPosition `protobuf:"bytes,3,opt,name=position,proto3" json:"position,omitempty"`
	XXX_NoUnkeyedLiteral struct{}        `json:"-"`
	XXX_unrecognized     []byte          `json:"-"`
	XXX_sizecache        int32           `json:"-"`
}

func (m *PlayerData) Reset()         { *m = PlayerData{} }
func (m *PlayerData) String() string { return proto.CompactTextString(m) }
func (*PlayerData) ProtoMessage()    {}
func (*PlayerData) Descriptor() ([]byte, []int) {
	return fileDescriptor_7ed0ab0be00c75b7, []int{0}
}

func (m *PlayerData) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_PlayerData.Unmarshal(m, b)
}
func (m *PlayerData) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_PlayerData.Marshal(b, m, deterministic)
}
func (m *PlayerData) XXX_Merge(src proto.Message) {
	xxx_messageInfo_PlayerData.Merge(m, src)
}
func (m *PlayerData) XXX_Size() int {
	return xxx_messageInfo_PlayerData.Size(m)
}
func (m *PlayerData) XXX_DiscardUnknown() {
	xxx_messageInfo_PlayerData.DiscardUnknown(m)
}

var xxx_messageInfo_PlayerData proto.InternalMessageInfo

func (m *PlayerData) GetId() int32 {
	if m != nil {
		return m.Id
	}
	return 0
}

func (m *PlayerData) GetName() string {
	if m != nil {
		return m.Name
	}
	return ""
}

func (m *PlayerData) GetPosition() *PlayerPosition {
	if m != nil {
		return m.Position
	}
	return nil
}

type ConnectRequest struct {
	Name                 string   `protobuf:"bytes,1,opt,name=name,proto3" json:"name,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *ConnectRequest) Reset()         { *m = ConnectRequest{} }
func (m *ConnectRequest) String() string { return proto.CompactTextString(m) }
func (*ConnectRequest) ProtoMessage()    {}
func (*ConnectRequest) Descriptor() ([]byte, []int) {
	return fileDescriptor_7ed0ab0be00c75b7, []int{1}
}

func (m *ConnectRequest) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_ConnectRequest.Unmarshal(m, b)
}
func (m *ConnectRequest) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_ConnectRequest.Marshal(b, m, deterministic)
}
func (m *ConnectRequest) XXX_Merge(src proto.Message) {
	xxx_messageInfo_ConnectRequest.Merge(m, src)
}
func (m *ConnectRequest) XXX_Size() int {
	return xxx_messageInfo_ConnectRequest.Size(m)
}
func (m *ConnectRequest) XXX_DiscardUnknown() {
	xxx_messageInfo_ConnectRequest.DiscardUnknown(m)
}

var xxx_messageInfo_ConnectRequest proto.InternalMessageInfo

func (m *ConnectRequest) GetName() string {
	if m != nil {
		return m.Name
	}
	return ""
}

type ConnectResponse struct {
	Player               *PlayerData   `protobuf:"bytes,1,opt,name=player,proto3" json:"player,omitempty"`
	IsSuccess            bool          `protobuf:"varint,2,opt,name=is_success,json=isSuccess,proto3" json:"is_success,omitempty"`
	OtherPlayers         []*PlayerData `protobuf:"bytes,3,rep,name=other_players,json=otherPlayers,proto3" json:"other_players,omitempty"`
	XXX_NoUnkeyedLiteral struct{}      `json:"-"`
	XXX_unrecognized     []byte        `json:"-"`
	XXX_sizecache        int32         `json:"-"`
}

func (m *ConnectResponse) Reset()         { *m = ConnectResponse{} }
func (m *ConnectResponse) String() string { return proto.CompactTextString(m) }
func (*ConnectResponse) ProtoMessage()    {}
func (*ConnectResponse) Descriptor() ([]byte, []int) {
	return fileDescriptor_7ed0ab0be00c75b7, []int{2}
}

func (m *ConnectResponse) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_ConnectResponse.Unmarshal(m, b)
}
func (m *ConnectResponse) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_ConnectResponse.Marshal(b, m, deterministic)
}
func (m *ConnectResponse) XXX_Merge(src proto.Message) {
	xxx_messageInfo_ConnectResponse.Merge(m, src)
}
func (m *ConnectResponse) XXX_Size() int {
	return xxx_messageInfo_ConnectResponse.Size(m)
}
func (m *ConnectResponse) XXX_DiscardUnknown() {
	xxx_messageInfo_ConnectResponse.DiscardUnknown(m)
}

var xxx_messageInfo_ConnectResponse proto.InternalMessageInfo

func (m *ConnectResponse) GetPlayer() *PlayerData {
	if m != nil {
		return m.Player
	}
	return nil
}

func (m *ConnectResponse) GetIsSuccess() bool {
	if m != nil {
		return m.IsSuccess
	}
	return false
}

func (m *ConnectResponse) GetOtherPlayers() []*PlayerData {
	if m != nil {
		return m.OtherPlayers
	}
	return nil
}

type PlayerPosition struct {
	X                    float32  `protobuf:"fixed32,1,opt,name=x,proto3" json:"x,omitempty"`
	Y                    float32  `protobuf:"fixed32,2,opt,name=y,proto3" json:"y,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *PlayerPosition) Reset()         { *m = PlayerPosition{} }
func (m *PlayerPosition) String() string { return proto.CompactTextString(m) }
func (*PlayerPosition) ProtoMessage()    {}
func (*PlayerPosition) Descriptor() ([]byte, []int) {
	return fileDescriptor_7ed0ab0be00c75b7, []int{3}
}

func (m *PlayerPosition) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_PlayerPosition.Unmarshal(m, b)
}
func (m *PlayerPosition) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_PlayerPosition.Marshal(b, m, deterministic)
}
func (m *PlayerPosition) XXX_Merge(src proto.Message) {
	xxx_messageInfo_PlayerPosition.Merge(m, src)
}
func (m *PlayerPosition) XXX_Size() int {
	return xxx_messageInfo_PlayerPosition.Size(m)
}
func (m *PlayerPosition) XXX_DiscardUnknown() {
	xxx_messageInfo_PlayerPosition.DiscardUnknown(m)
}

var xxx_messageInfo_PlayerPosition proto.InternalMessageInfo

func (m *PlayerPosition) GetX() float32 {
	if m != nil {
		return m.X
	}
	return 0
}

func (m *PlayerPosition) GetY() float32 {
	if m != nil {
		return m.Y
	}
	return 0
}

type PlayerPositionById struct {
	Id                   int32           `protobuf:"varint,1,opt,name=id,proto3" json:"id,omitempty"`
	Position             *PlayerPosition `protobuf:"bytes,2,opt,name=position,proto3" json:"position,omitempty"`
	XXX_NoUnkeyedLiteral struct{}        `json:"-"`
	XXX_unrecognized     []byte          `json:"-"`
	XXX_sizecache        int32           `json:"-"`
}

func (m *PlayerPositionById) Reset()         { *m = PlayerPositionById{} }
func (m *PlayerPositionById) String() string { return proto.CompactTextString(m) }
func (*PlayerPositionById) ProtoMessage()    {}
func (*PlayerPositionById) Descriptor() ([]byte, []int) {
	return fileDescriptor_7ed0ab0be00c75b7, []int{4}
}

func (m *PlayerPositionById) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_PlayerPositionById.Unmarshal(m, b)
}
func (m *PlayerPositionById) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_PlayerPositionById.Marshal(b, m, deterministic)
}
func (m *PlayerPositionById) XXX_Merge(src proto.Message) {
	xxx_messageInfo_PlayerPositionById.Merge(m, src)
}
func (m *PlayerPositionById) XXX_Size() int {
	return xxx_messageInfo_PlayerPositionById.Size(m)
}
func (m *PlayerPositionById) XXX_DiscardUnknown() {
	xxx_messageInfo_PlayerPositionById.DiscardUnknown(m)
}

var xxx_messageInfo_PlayerPositionById proto.InternalMessageInfo

func (m *PlayerPositionById) GetId() int32 {
	if m != nil {
		return m.Id
	}
	return 0
}

func (m *PlayerPositionById) GetPosition() *PlayerPosition {
	if m != nil {
		return m.Position
	}
	return nil
}

type StreamResponse struct {
	// Types that are valid to be assigned to Event:
	//	*StreamResponse_Player
	//	*StreamResponse_PlayerPositionById
	Event                isStreamResponse_Event `protobuf_oneof:"event"`
	XXX_NoUnkeyedLiteral struct{}               `json:"-"`
	XXX_unrecognized     []byte                 `json:"-"`
	XXX_sizecache        int32                  `json:"-"`
}

func (m *StreamResponse) Reset()         { *m = StreamResponse{} }
func (m *StreamResponse) String() string { return proto.CompactTextString(m) }
func (*StreamResponse) ProtoMessage()    {}
func (*StreamResponse) Descriptor() ([]byte, []int) {
	return fileDescriptor_7ed0ab0be00c75b7, []int{5}
}

func (m *StreamResponse) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_StreamResponse.Unmarshal(m, b)
}
func (m *StreamResponse) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_StreamResponse.Marshal(b, m, deterministic)
}
func (m *StreamResponse) XXX_Merge(src proto.Message) {
	xxx_messageInfo_StreamResponse.Merge(m, src)
}
func (m *StreamResponse) XXX_Size() int {
	return xxx_messageInfo_StreamResponse.Size(m)
}
func (m *StreamResponse) XXX_DiscardUnknown() {
	xxx_messageInfo_StreamResponse.DiscardUnknown(m)
}

var xxx_messageInfo_StreamResponse proto.InternalMessageInfo

type isStreamResponse_Event interface {
	isStreamResponse_Event()
}

type StreamResponse_Player struct {
	Player *StreamResponse_PlayerConnected `protobuf:"bytes,1,opt,name=player,proto3,oneof"`
}

type StreamResponse_PlayerPositionById struct {
	PlayerPositionById *PlayerPositionById `protobuf:"bytes,2,opt,name=player_position_by_id,json=playerPositionById,proto3,oneof"`
}

func (*StreamResponse_Player) isStreamResponse_Event() {}

func (*StreamResponse_PlayerPositionById) isStreamResponse_Event() {}

func (m *StreamResponse) GetEvent() isStreamResponse_Event {
	if m != nil {
		return m.Event
	}
	return nil
}

func (m *StreamResponse) GetPlayer() *StreamResponse_PlayerConnected {
	if x, ok := m.GetEvent().(*StreamResponse_Player); ok {
		return x.Player
	}
	return nil
}

func (m *StreamResponse) GetPlayerPositionById() *PlayerPositionById {
	if x, ok := m.GetEvent().(*StreamResponse_PlayerPositionById); ok {
		return x.PlayerPositionById
	}
	return nil
}

// XXX_OneofFuncs is for the internal use of the proto package.
func (*StreamResponse) XXX_OneofFuncs() (func(msg proto.Message, b *proto.Buffer) error, func(msg proto.Message, tag, wire int, b *proto.Buffer) (bool, error), func(msg proto.Message) (n int), []interface{}) {
	return _StreamResponse_OneofMarshaler, _StreamResponse_OneofUnmarshaler, _StreamResponse_OneofSizer, []interface{}{
		(*StreamResponse_Player)(nil),
		(*StreamResponse_PlayerPositionById)(nil),
	}
}

func _StreamResponse_OneofMarshaler(msg proto.Message, b *proto.Buffer) error {
	m := msg.(*StreamResponse)
	// event
	switch x := m.Event.(type) {
	case *StreamResponse_Player:
		b.EncodeVarint(1<<3 | proto.WireBytes)
		if err := b.EncodeMessage(x.Player); err != nil {
			return err
		}
	case *StreamResponse_PlayerPositionById:
		b.EncodeVarint(2<<3 | proto.WireBytes)
		if err := b.EncodeMessage(x.PlayerPositionById); err != nil {
			return err
		}
	case nil:
	default:
		return fmt.Errorf("StreamResponse.Event has unexpected type %T", x)
	}
	return nil
}

func _StreamResponse_OneofUnmarshaler(msg proto.Message, tag, wire int, b *proto.Buffer) (bool, error) {
	m := msg.(*StreamResponse)
	switch tag {
	case 1: // event.player
		if wire != proto.WireBytes {
			return true, proto.ErrInternalBadWireType
		}
		msg := new(StreamResponse_PlayerConnected)
		err := b.DecodeMessage(msg)
		m.Event = &StreamResponse_Player{msg}
		return true, err
	case 2: // event.player_position_by_id
		if wire != proto.WireBytes {
			return true, proto.ErrInternalBadWireType
		}
		msg := new(PlayerPositionById)
		err := b.DecodeMessage(msg)
		m.Event = &StreamResponse_PlayerPositionById{msg}
		return true, err
	default:
		return false, nil
	}
}

func _StreamResponse_OneofSizer(msg proto.Message) (n int) {
	m := msg.(*StreamResponse)
	// event
	switch x := m.Event.(type) {
	case *StreamResponse_Player:
		s := proto.Size(x.Player)
		n += 1 // tag and wire
		n += proto.SizeVarint(uint64(s))
		n += s
	case *StreamResponse_PlayerPositionById:
		s := proto.Size(x.PlayerPositionById)
		n += 1 // tag and wire
		n += proto.SizeVarint(uint64(s))
		n += s
	case nil:
	default:
		panic(fmt.Sprintf("proto: unexpected type %T in oneof", x))
	}
	return n
}

type StreamResponse_PlayerConnected struct {
	Id                   int32    `protobuf:"varint,1,opt,name=id,proto3" json:"id,omitempty"`
	Name                 string   `protobuf:"bytes,2,opt,name=name,proto3" json:"name,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *StreamResponse_PlayerConnected) Reset()         { *m = StreamResponse_PlayerConnected{} }
func (m *StreamResponse_PlayerConnected) String() string { return proto.CompactTextString(m) }
func (*StreamResponse_PlayerConnected) ProtoMessage()    {}
func (*StreamResponse_PlayerConnected) Descriptor() ([]byte, []int) {
	return fileDescriptor_7ed0ab0be00c75b7, []int{5, 0}
}

func (m *StreamResponse_PlayerConnected) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_StreamResponse_PlayerConnected.Unmarshal(m, b)
}
func (m *StreamResponse_PlayerConnected) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_StreamResponse_PlayerConnected.Marshal(b, m, deterministic)
}
func (m *StreamResponse_PlayerConnected) XXX_Merge(src proto.Message) {
	xxx_messageInfo_StreamResponse_PlayerConnected.Merge(m, src)
}
func (m *StreamResponse_PlayerConnected) XXX_Size() int {
	return xxx_messageInfo_StreamResponse_PlayerConnected.Size(m)
}
func (m *StreamResponse_PlayerConnected) XXX_DiscardUnknown() {
	xxx_messageInfo_StreamResponse_PlayerConnected.DiscardUnknown(m)
}

var xxx_messageInfo_StreamResponse_PlayerConnected proto.InternalMessageInfo

func (m *StreamResponse_PlayerConnected) GetId() int32 {
	if m != nil {
		return m.Id
	}
	return 0
}

func (m *StreamResponse_PlayerConnected) GetName() string {
	if m != nil {
		return m.Name
	}
	return ""
}

func init() {
	proto.RegisterType((*PlayerData)(nil), "platformer.PlayerData")
	proto.RegisterType((*ConnectRequest)(nil), "platformer.ConnectRequest")
	proto.RegisterType((*ConnectResponse)(nil), "platformer.ConnectResponse")
	proto.RegisterType((*PlayerPosition)(nil), "platformer.PlayerPosition")
	proto.RegisterType((*PlayerPositionById)(nil), "platformer.PlayerPositionById")
	proto.RegisterType((*StreamResponse)(nil), "platformer.StreamResponse")
	proto.RegisterType((*StreamResponse_PlayerConnected)(nil), "platformer.StreamResponse.PlayerConnected")
}

func init() { proto.RegisterFile("platformer.proto", fileDescriptor_7ed0ab0be00c75b7) }

var fileDescriptor_7ed0ab0be00c75b7 = []byte{
	// 382 bytes of a gzipped FileDescriptorProto
	0x1f, 0x8b, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0xff, 0x94, 0x93, 0xcd, 0x4e, 0xc2, 0x40,
	0x14, 0x85, 0x99, 0x22, 0x7f, 0x17, 0x2c, 0x66, 0x12, 0x4d, 0x53, 0xa3, 0x69, 0x1a, 0x17, 0x8d,
	0x31, 0xc4, 0x60, 0x74, 0xe3, 0x0e, 0x59, 0xe0, 0x8e, 0x0c, 0x5b, 0x93, 0xa6, 0xd0, 0x31, 0x4c,
	0x02, 0x9d, 0xda, 0x19, 0x0c, 0x7d, 0x15, 0x13, 0x9f, 0xd0, 0x97, 0x30, 0x4c, 0xc7, 0x42, 0x41,
	0xfc, 0xd9, 0x0d, 0x97, 0x93, 0x73, 0xce, 0xfd, 0x72, 0x0b, 0x47, 0xf1, 0x2c, 0x90, 0xcf, 0x3c,
	0x99, 0xd3, 0xa4, 0x13, 0x27, 0x5c, 0x72, 0x0c, 0xeb, 0x89, 0x3b, 0x05, 0x18, 0xce, 0x82, 0x94,
	0x26, 0xfd, 0x40, 0x06, 0xd8, 0x04, 0x83, 0x85, 0x16, 0x72, 0x90, 0x57, 0x21, 0x06, 0x0b, 0x31,
	0x86, 0x83, 0x28, 0x98, 0x53, 0xcb, 0x70, 0x90, 0xd7, 0x20, 0xea, 0x8d, 0xef, 0xa0, 0x1e, 0x73,
	0xc1, 0x24, 0xe3, 0x91, 0x55, 0x76, 0x90, 0xd7, 0xec, 0xda, 0x9d, 0x8d, 0x88, 0xcc, 0x6d, 0xa8,
	0x15, 0x24, 0xd7, 0xba, 0x17, 0x60, 0x3e, 0xf0, 0x28, 0xa2, 0x13, 0x49, 0xe8, 0xcb, 0x82, 0x0a,
	0x99, 0xbb, 0xa3, 0xb5, 0xbb, 0xfb, 0x8e, 0xa0, 0x9d, 0xcb, 0x44, 0xcc, 0x23, 0x41, 0x71, 0x07,
	0xaa, 0xb1, 0x72, 0x55, 0xca, 0x66, 0xf7, 0x64, 0x37, 0x6f, 0xd5, 0x9e, 0x68, 0x15, 0x3e, 0x03,
	0x60, 0xc2, 0x17, 0x8b, 0xc9, 0x84, 0x0a, 0xa1, 0xba, 0xd7, 0x49, 0x83, 0x89, 0x51, 0x36, 0xc0,
	0xf7, 0x70, 0xc8, 0xe5, 0x94, 0x26, 0x7e, 0x26, 0x17, 0x56, 0xd9, 0x29, 0xff, 0xe0, 0xda, 0x52,
	0xe2, 0x6c, 0x20, 0xdc, 0x2b, 0x30, 0x8b, 0x1b, 0xe2, 0x16, 0xa0, 0xa5, 0x2a, 0x66, 0x10, 0xb4,
	0x5c, 0xfd, 0x4a, 0x55, 0xa4, 0x41, 0x50, 0xea, 0x3e, 0x01, 0x2e, 0xaa, 0x7b, 0xe9, 0x63, 0xb8,
	0x43, 0x79, 0x93, 0xa8, 0xf1, 0x0f, 0xa2, 0x1f, 0x08, 0xcc, 0x91, 0x4c, 0x68, 0x30, 0xcf, 0x51,
	0xf5, 0xb7, 0x50, 0x5d, 0x6e, 0x1a, 0x15, 0xb5, 0xda, 0x57, 0xc3, 0xa6, 0xe1, 0xa0, 0x94, 0x03,
	0x1c, 0xc1, 0x71, 0xf6, 0xf2, 0xbf, 0xb2, 0xfc, 0x71, 0xea, 0xb3, 0x50, 0xb7, 0x3b, 0xdf, 0xdf,
	0x6e, 0xb5, 0xdf, 0xa0, 0x44, 0x70, 0xbc, 0x33, 0xb5, 0x6f, 0xa1, 0xbd, 0x95, 0xf8, 0x97, 0x73,
	0xeb, 0xd5, 0xa0, 0x42, 0x5f, 0x69, 0x24, 0xbb, 0x6f, 0x48, 0x9d, 0xaa, 0xce, 0xc5, 0x3d, 0xa8,
	0x69, 0x23, 0x5c, 0xa0, 0x55, 0xbc, 0x31, 0xfb, 0xf4, 0xdb, 0xff, 0x34, 0xad, 0x01, 0x54, 0x33,
	0x26, 0xf8, 0x97, 0x95, 0x6c, 0x7b, 0x3f, 0x47, 0x0f, 0x5d, 0xa3, 0x71, 0x55, 0x7d, 0x59, 0x37,
	0x9f, 0x01, 0x00, 0x00, 0xff, 0xff, 0xe8, 0x99, 0x8c, 0x67, 0x6d, 0x03, 0x00, 0x00,
}

// Reference imports to suppress errors if they are not otherwise used.
var _ context.Context
var _ grpc.ClientConn

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
const _ = grpc.SupportPackageIsVersion4

// PlatformerClient is the client API for Platformer service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://godoc.org/google.golang.org/grpc#ClientConn.NewStream.
type PlatformerClient interface {
	Connect(ctx context.Context, in *ConnectRequest, opts ...grpc.CallOption) (*ConnectResponse, error)
	Stream(ctx context.Context, opts ...grpc.CallOption) (Platformer_StreamClient, error)
}

type platformerClient struct {
	cc *grpc.ClientConn
}

func NewPlatformerClient(cc *grpc.ClientConn) PlatformerClient {
	return &platformerClient{cc}
}

func (c *platformerClient) Connect(ctx context.Context, in *ConnectRequest, opts ...grpc.CallOption) (*ConnectResponse, error) {
	out := new(ConnectResponse)
	err := c.cc.Invoke(ctx, "/platformer.Platformer/Connect", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *platformerClient) Stream(ctx context.Context, opts ...grpc.CallOption) (Platformer_StreamClient, error) {
	stream, err := c.cc.NewStream(ctx, &_Platformer_serviceDesc.Streams[0], "/platformer.Platformer/Stream", opts...)
	if err != nil {
		return nil, err
	}
	x := &platformerStreamClient{stream}
	return x, nil
}

type Platformer_StreamClient interface {
	Send(*PlayerPositionById) error
	Recv() (*StreamResponse, error)
	grpc.ClientStream
}

type platformerStreamClient struct {
	grpc.ClientStream
}

func (x *platformerStreamClient) Send(m *PlayerPositionById) error {
	return x.ClientStream.SendMsg(m)
}

func (x *platformerStreamClient) Recv() (*StreamResponse, error) {
	m := new(StreamResponse)
	if err := x.ClientStream.RecvMsg(m); err != nil {
		return nil, err
	}
	return m, nil
}

// PlatformerServer is the server API for Platformer service.
type PlatformerServer interface {
	Connect(context.Context, *ConnectRequest) (*ConnectResponse, error)
	Stream(Platformer_StreamServer) error
}

func RegisterPlatformerServer(s *grpc.Server, srv PlatformerServer) {
	s.RegisterService(&_Platformer_serviceDesc, srv)
}

func _Platformer_Connect_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(ConnectRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(PlatformerServer).Connect(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/platformer.Platformer/Connect",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(PlatformerServer).Connect(ctx, req.(*ConnectRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _Platformer_Stream_Handler(srv interface{}, stream grpc.ServerStream) error {
	return srv.(PlatformerServer).Stream(&platformerStreamServer{stream})
}

type Platformer_StreamServer interface {
	Send(*StreamResponse) error
	Recv() (*PlayerPositionById, error)
	grpc.ServerStream
}

type platformerStreamServer struct {
	grpc.ServerStream
}

func (x *platformerStreamServer) Send(m *StreamResponse) error {
	return x.ServerStream.SendMsg(m)
}

func (x *platformerStreamServer) Recv() (*PlayerPositionById, error) {
	m := new(PlayerPositionById)
	if err := x.ServerStream.RecvMsg(m); err != nil {
		return nil, err
	}
	return m, nil
}

var _Platformer_serviceDesc = grpc.ServiceDesc{
	ServiceName: "platformer.Platformer",
	HandlerType: (*PlatformerServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "Connect",
			Handler:    _Platformer_Connect_Handler,
		},
	},
	Streams: []grpc.StreamDesc{
		{
			StreamName:    "Stream",
			Handler:       _Platformer_Stream_Handler,
			ServerStreams: true,
			ClientStreams: true,
		},
	},
	Metadata: "platformer.proto",
}