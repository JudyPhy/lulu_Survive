import * as $protobuf from "protobufjs";
/** Namespace pb. */
export namespace pb {

    /** PayMode enum. */
    enum PayMode {
        SHARE = 0,
        OWNER = 1
    }

    /** GameRound enum. */
    enum GameRound {
        ROUND4 = 0,
        ROUND8 = 1
    }

    /** Properties of a PlayerInfo. */
    interface IPlayerInfo {

        /** PlayerInfo playerId */
        playerId: (number|Long);

        /** PlayerInfo nickname */
        nickname: string;

        /** PlayerInfo headicon */
        headicon: string;

        /** PlayerInfo card */
        card: (number|Long);

        /** PlayerInfo coin */
        coin: (number|Long);
    }

    /** Represents a PlayerInfo. */
    class PlayerInfo implements IPlayerInfo {

        /**
         * Constructs a new PlayerInfo.
         * @param [properties] Properties to set
         */
        constructor(properties?: pb.IPlayerInfo);

        /** PlayerInfo playerId. */
        public playerId: (number|Long);

        /** PlayerInfo nickname. */
        public nickname: string;

        /** PlayerInfo headicon. */
        public headicon: string;

        /** PlayerInfo card. */
        public card: (number|Long);

        /** PlayerInfo coin. */
        public coin: (number|Long);

        /**
         * Creates a new PlayerInfo instance using the specified properties.
         * @param [properties] Properties to set
         * @returns PlayerInfo instance
         */
        public static create(properties?: pb.IPlayerInfo): pb.PlayerInfo;

        /**
         * Encodes the specified PlayerInfo message. Does not implicitly {@link pb.PlayerInfo.verify|verify} messages.
         * @param message PlayerInfo message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encode(message: pb.IPlayerInfo, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Encodes the specified PlayerInfo message, length delimited. Does not implicitly {@link pb.PlayerInfo.verify|verify} messages.
         * @param message PlayerInfo message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encodeDelimited(message: pb.IPlayerInfo, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Decodes a PlayerInfo message from the specified reader or buffer.
         * @param reader Reader or buffer to decode from
         * @param [length] Message length if known beforehand
         * @returns PlayerInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): pb.PlayerInfo;

        /**
         * Decodes a PlayerInfo message from the specified reader or buffer, length delimited.
         * @param reader Reader or buffer to decode from
         * @returns PlayerInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): pb.PlayerInfo;

        /**
         * Verifies a PlayerInfo message.
         * @param message Plain object to verify
         * @returns `null` if valid, otherwise the reason why it is not
         */
        public static verify(message: { [k: string]: any }): (string|null);

        /**
         * Creates a PlayerInfo message from a plain object. Also converts values to their respective internal types.
         * @param object Plain object
         * @returns PlayerInfo
         */
        public static fromObject(object: { [k: string]: any }): pb.PlayerInfo;

        /**
         * Creates a plain object from a PlayerInfo message. Also converts values to other types if specified.
         * @param message PlayerInfo
         * @param [options] Conversion options
         * @returns Plain object
         */
        public static toObject(message: pb.PlayerInfo, options?: $protobuf.IConversionOptions): { [k: string]: any };

        /**
         * Converts this PlayerInfo to JSON.
         * @returns JSON object
         */
        public toJSON(): { [k: string]: any };
    }

    /** Properties of a RoomInfo. */
    interface IRoomInfo {

        /** RoomInfo roomId */
        roomId: (number|Long);

        /** RoomInfo password */
        password: string;

        /** RoomInfo pay */
        pay: pb.PayMode;

        /** RoomInfo round */
        round: pb.GameRound;

        /** RoomInfo exitPay */
        exitPay: boolean;
    }

    /** Represents a RoomInfo. */
    class RoomInfo implements IRoomInfo {

        /**
         * Constructs a new RoomInfo.
         * @param [properties] Properties to set
         */
        constructor(properties?: pb.IRoomInfo);

        /** RoomInfo roomId. */
        public roomId: (number|Long);

        /** RoomInfo password. */
        public password: string;

        /** RoomInfo pay. */
        public pay: pb.PayMode;

        /** RoomInfo round. */
        public round: pb.GameRound;

        /** RoomInfo exitPay. */
        public exitPay: boolean;

        /**
         * Creates a new RoomInfo instance using the specified properties.
         * @param [properties] Properties to set
         * @returns RoomInfo instance
         */
        public static create(properties?: pb.IRoomInfo): pb.RoomInfo;

        /**
         * Encodes the specified RoomInfo message. Does not implicitly {@link pb.RoomInfo.verify|verify} messages.
         * @param message RoomInfo message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encode(message: pb.IRoomInfo, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Encodes the specified RoomInfo message, length delimited. Does not implicitly {@link pb.RoomInfo.verify|verify} messages.
         * @param message RoomInfo message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encodeDelimited(message: pb.IRoomInfo, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Decodes a RoomInfo message from the specified reader or buffer.
         * @param reader Reader or buffer to decode from
         * @param [length] Message length if known beforehand
         * @returns RoomInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): pb.RoomInfo;

        /**
         * Decodes a RoomInfo message from the specified reader or buffer, length delimited.
         * @param reader Reader or buffer to decode from
         * @returns RoomInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): pb.RoomInfo;

        /**
         * Verifies a RoomInfo message.
         * @param message Plain object to verify
         * @returns `null` if valid, otherwise the reason why it is not
         */
        public static verify(message: { [k: string]: any }): (string|null);

        /**
         * Creates a RoomInfo message from a plain object. Also converts values to their respective internal types.
         * @param object Plain object
         * @returns RoomInfo
         */
        public static fromObject(object: { [k: string]: any }): pb.RoomInfo;

        /**
         * Creates a plain object from a RoomInfo message. Also converts values to other types if specified.
         * @param message RoomInfo
         * @param [options] Conversion options
         * @returns Plain object
         */
        public static toObject(message: pb.RoomInfo, options?: $protobuf.IConversionOptions): { [k: string]: any };

        /**
         * Converts this RoomInfo to JSON.
         * @returns JSON object
         */
        public toJSON(): { [k: string]: any };
    }

    /** Properties of a C2GSLogin. */
    interface IC2GSLogin {

        /** C2GSLogin user */
        user: string;

        /** C2GSLogin password */
        password: string;
    }

    /** Represents a C2GSLogin. */
    class C2GSLogin implements IC2GSLogin {

        /**
         * Constructs a new C2GSLogin.
         * @param [properties] Properties to set
         */
        constructor(properties?: pb.IC2GSLogin);

        /** C2GSLogin user. */
        public user: string;

        /** C2GSLogin password. */
        public password: string;

        /**
         * Creates a new C2GSLogin instance using the specified properties.
         * @param [properties] Properties to set
         * @returns C2GSLogin instance
         */
        public static create(properties?: pb.IC2GSLogin): pb.C2GSLogin;

        /**
         * Encodes the specified C2GSLogin message. Does not implicitly {@link pb.C2GSLogin.verify|verify} messages.
         * @param message C2GSLogin message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encode(message: pb.IC2GSLogin, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Encodes the specified C2GSLogin message, length delimited. Does not implicitly {@link pb.C2GSLogin.verify|verify} messages.
         * @param message C2GSLogin message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encodeDelimited(message: pb.IC2GSLogin, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Decodes a C2GSLogin message from the specified reader or buffer.
         * @param reader Reader or buffer to decode from
         * @param [length] Message length if known beforehand
         * @returns C2GSLogin
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): pb.C2GSLogin;

        /**
         * Decodes a C2GSLogin message from the specified reader or buffer, length delimited.
         * @param reader Reader or buffer to decode from
         * @returns C2GSLogin
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): pb.C2GSLogin;

        /**
         * Verifies a C2GSLogin message.
         * @param message Plain object to verify
         * @returns `null` if valid, otherwise the reason why it is not
         */
        public static verify(message: { [k: string]: any }): (string|null);

        /**
         * Creates a C2GSLogin message from a plain object. Also converts values to their respective internal types.
         * @param object Plain object
         * @returns C2GSLogin
         */
        public static fromObject(object: { [k: string]: any }): pb.C2GSLogin;

        /**
         * Creates a plain object from a C2GSLogin message. Also converts values to other types if specified.
         * @param message C2GSLogin
         * @param [options] Conversion options
         * @returns Plain object
         */
        public static toObject(message: pb.C2GSLogin, options?: $protobuf.IConversionOptions): { [k: string]: any };

        /**
         * Converts this C2GSLogin to JSON.
         * @returns JSON object
         */
        public toJSON(): { [k: string]: any };
    }

    /** Properties of a GS2CLoginRet. */
    interface IGS2CLoginRet {

        /** GS2CLoginRet user */
        user: pb.IPlayerInfo;

        /** GS2CLoginRet errorCode */
        errorCode: pb.GS2CLoginRet.ErrorCode;
    }

    /** Represents a GS2CLoginRet. */
    class GS2CLoginRet implements IGS2CLoginRet {

        /**
         * Constructs a new GS2CLoginRet.
         * @param [properties] Properties to set
         */
        constructor(properties?: pb.IGS2CLoginRet);

        /** GS2CLoginRet user. */
        public user: pb.IPlayerInfo;

        /** GS2CLoginRet errorCode. */
        public errorCode: pb.GS2CLoginRet.ErrorCode;

        /**
         * Creates a new GS2CLoginRet instance using the specified properties.
         * @param [properties] Properties to set
         * @returns GS2CLoginRet instance
         */
        public static create(properties?: pb.IGS2CLoginRet): pb.GS2CLoginRet;

        /**
         * Encodes the specified GS2CLoginRet message. Does not implicitly {@link pb.GS2CLoginRet.verify|verify} messages.
         * @param message GS2CLoginRet message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encode(message: pb.IGS2CLoginRet, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Encodes the specified GS2CLoginRet message, length delimited. Does not implicitly {@link pb.GS2CLoginRet.verify|verify} messages.
         * @param message GS2CLoginRet message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encodeDelimited(message: pb.IGS2CLoginRet, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Decodes a GS2CLoginRet message from the specified reader or buffer.
         * @param reader Reader or buffer to decode from
         * @param [length] Message length if known beforehand
         * @returns GS2CLoginRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): pb.GS2CLoginRet;

        /**
         * Decodes a GS2CLoginRet message from the specified reader or buffer, length delimited.
         * @param reader Reader or buffer to decode from
         * @returns GS2CLoginRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): pb.GS2CLoginRet;

        /**
         * Verifies a GS2CLoginRet message.
         * @param message Plain object to verify
         * @returns `null` if valid, otherwise the reason why it is not
         */
        public static verify(message: { [k: string]: any }): (string|null);

        /**
         * Creates a GS2CLoginRet message from a plain object. Also converts values to their respective internal types.
         * @param object Plain object
         * @returns GS2CLoginRet
         */
        public static fromObject(object: { [k: string]: any }): pb.GS2CLoginRet;

        /**
         * Creates a plain object from a GS2CLoginRet message. Also converts values to other types if specified.
         * @param message GS2CLoginRet
         * @param [options] Conversion options
         * @returns Plain object
         */
        public static toObject(message: pb.GS2CLoginRet, options?: $protobuf.IConversionOptions): { [k: string]: any };

        /**
         * Converts this GS2CLoginRet to JSON.
         * @returns JSON object
         */
        public toJSON(): { [k: string]: any };
    }

    namespace GS2CLoginRet {

        /** ErrorCode enum. */
        enum ErrorCode {
            Success = 1,
            Fail = 2
        }
    }

    /** Properties of a C2GSCreateRoom. */
    interface IC2GSCreateRoom {

        /** C2GSCreateRoom pay */
        pay: pb.PayMode;

        /** C2GSCreateRoom round */
        round: pb.GameRound;

        /** C2GSCreateRoom exitPay */
        exitPay: boolean;

        /** C2GSCreateRoom password */
        password: (number|Long);
    }

    /** Represents a C2GSCreateRoom. */
    class C2GSCreateRoom implements IC2GSCreateRoom {

        /**
         * Constructs a new C2GSCreateRoom.
         * @param [properties] Properties to set
         */
        constructor(properties?: pb.IC2GSCreateRoom);

        /** C2GSCreateRoom pay. */
        public pay: pb.PayMode;

        /** C2GSCreateRoom round. */
        public round: pb.GameRound;

        /** C2GSCreateRoom exitPay. */
        public exitPay: boolean;

        /** C2GSCreateRoom password. */
        public password: (number|Long);

        /**
         * Creates a new C2GSCreateRoom instance using the specified properties.
         * @param [properties] Properties to set
         * @returns C2GSCreateRoom instance
         */
        public static create(properties?: pb.IC2GSCreateRoom): pb.C2GSCreateRoom;

        /**
         * Encodes the specified C2GSCreateRoom message. Does not implicitly {@link pb.C2GSCreateRoom.verify|verify} messages.
         * @param message C2GSCreateRoom message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encode(message: pb.IC2GSCreateRoom, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Encodes the specified C2GSCreateRoom message, length delimited. Does not implicitly {@link pb.C2GSCreateRoom.verify|verify} messages.
         * @param message C2GSCreateRoom message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encodeDelimited(message: pb.IC2GSCreateRoom, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Decodes a C2GSCreateRoom message from the specified reader or buffer.
         * @param reader Reader or buffer to decode from
         * @param [length] Message length if known beforehand
         * @returns C2GSCreateRoom
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): pb.C2GSCreateRoom;

        /**
         * Decodes a C2GSCreateRoom message from the specified reader or buffer, length delimited.
         * @param reader Reader or buffer to decode from
         * @returns C2GSCreateRoom
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): pb.C2GSCreateRoom;

        /**
         * Verifies a C2GSCreateRoom message.
         * @param message Plain object to verify
         * @returns `null` if valid, otherwise the reason why it is not
         */
        public static verify(message: { [k: string]: any }): (string|null);

        /**
         * Creates a C2GSCreateRoom message from a plain object. Also converts values to their respective internal types.
         * @param object Plain object
         * @returns C2GSCreateRoom
         */
        public static fromObject(object: { [k: string]: any }): pb.C2GSCreateRoom;

        /**
         * Creates a plain object from a C2GSCreateRoom message. Also converts values to other types if specified.
         * @param message C2GSCreateRoom
         * @param [options] Conversion options
         * @returns Plain object
         */
        public static toObject(message: pb.C2GSCreateRoom, options?: $protobuf.IConversionOptions): { [k: string]: any };

        /**
         * Converts this C2GSCreateRoom to JSON.
         * @returns JSON object
         */
        public toJSON(): { [k: string]: any };
    }

    /** Properties of a C2GSEnterRoom. */
    interface IC2GSEnterRoom {

        /** C2GSEnterRoom roomId */
        roomId?: (number|Long|null);

        /** C2GSEnterRoom password */
        password?: (number|Long|null);
    }

    /** Represents a C2GSEnterRoom. */
    class C2GSEnterRoom implements IC2GSEnterRoom {

        /**
         * Constructs a new C2GSEnterRoom.
         * @param [properties] Properties to set
         */
        constructor(properties?: pb.IC2GSEnterRoom);

        /** C2GSEnterRoom roomId. */
        public roomId: (number|Long);

        /** C2GSEnterRoom password. */
        public password: (number|Long);

        /**
         * Creates a new C2GSEnterRoom instance using the specified properties.
         * @param [properties] Properties to set
         * @returns C2GSEnterRoom instance
         */
        public static create(properties?: pb.IC2GSEnterRoom): pb.C2GSEnterRoom;

        /**
         * Encodes the specified C2GSEnterRoom message. Does not implicitly {@link pb.C2GSEnterRoom.verify|verify} messages.
         * @param message C2GSEnterRoom message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encode(message: pb.IC2GSEnterRoom, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Encodes the specified C2GSEnterRoom message, length delimited. Does not implicitly {@link pb.C2GSEnterRoom.verify|verify} messages.
         * @param message C2GSEnterRoom message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encodeDelimited(message: pb.IC2GSEnterRoom, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Decodes a C2GSEnterRoom message from the specified reader or buffer.
         * @param reader Reader or buffer to decode from
         * @param [length] Message length if known beforehand
         * @returns C2GSEnterRoom
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): pb.C2GSEnterRoom;

        /**
         * Decodes a C2GSEnterRoom message from the specified reader or buffer, length delimited.
         * @param reader Reader or buffer to decode from
         * @returns C2GSEnterRoom
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): pb.C2GSEnterRoom;

        /**
         * Verifies a C2GSEnterRoom message.
         * @param message Plain object to verify
         * @returns `null` if valid, otherwise the reason why it is not
         */
        public static verify(message: { [k: string]: any }): (string|null);

        /**
         * Creates a C2GSEnterRoom message from a plain object. Also converts values to their respective internal types.
         * @param object Plain object
         * @returns C2GSEnterRoom
         */
        public static fromObject(object: { [k: string]: any }): pb.C2GSEnterRoom;

        /**
         * Creates a plain object from a C2GSEnterRoom message. Also converts values to other types if specified.
         * @param message C2GSEnterRoom
         * @param [options] Conversion options
         * @returns Plain object
         */
        public static toObject(message: pb.C2GSEnterRoom, options?: $protobuf.IConversionOptions): { [k: string]: any };

        /**
         * Converts this C2GSEnterRoom to JSON.
         * @returns JSON object
         */
        public toJSON(): { [k: string]: any };
    }

    /** Properties of a GS2CEnterRoomRet. */
    interface IGS2CEnterRoomRet {

        /** GS2CEnterRoomRet roomId */
        roomId: (number|Long);

        /** GS2CEnterRoomRet errorCode */
        errorCode: pb.GS2CEnterRoomRet.ErrorCode;
    }

    /** Represents a GS2CEnterRoomRet. */
    class GS2CEnterRoomRet implements IGS2CEnterRoomRet {

        /**
         * Constructs a new GS2CEnterRoomRet.
         * @param [properties] Properties to set
         */
        constructor(properties?: pb.IGS2CEnterRoomRet);

        /** GS2CEnterRoomRet roomId. */
        public roomId: (number|Long);

        /** GS2CEnterRoomRet errorCode. */
        public errorCode: pb.GS2CEnterRoomRet.ErrorCode;

        /**
         * Creates a new GS2CEnterRoomRet instance using the specified properties.
         * @param [properties] Properties to set
         * @returns GS2CEnterRoomRet instance
         */
        public static create(properties?: pb.IGS2CEnterRoomRet): pb.GS2CEnterRoomRet;

        /**
         * Encodes the specified GS2CEnterRoomRet message. Does not implicitly {@link pb.GS2CEnterRoomRet.verify|verify} messages.
         * @param message GS2CEnterRoomRet message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encode(message: pb.IGS2CEnterRoomRet, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Encodes the specified GS2CEnterRoomRet message, length delimited. Does not implicitly {@link pb.GS2CEnterRoomRet.verify|verify} messages.
         * @param message GS2CEnterRoomRet message or plain object to encode
         * @param [writer] Writer to encode to
         * @returns Writer
         */
        public static encodeDelimited(message: pb.IGS2CEnterRoomRet, writer?: $protobuf.Writer): $protobuf.Writer;

        /**
         * Decodes a GS2CEnterRoomRet message from the specified reader or buffer.
         * @param reader Reader or buffer to decode from
         * @param [length] Message length if known beforehand
         * @returns GS2CEnterRoomRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): pb.GS2CEnterRoomRet;

        /**
         * Decodes a GS2CEnterRoomRet message from the specified reader or buffer, length delimited.
         * @param reader Reader or buffer to decode from
         * @returns GS2CEnterRoomRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): pb.GS2CEnterRoomRet;

        /**
         * Verifies a GS2CEnterRoomRet message.
         * @param message Plain object to verify
         * @returns `null` if valid, otherwise the reason why it is not
         */
        public static verify(message: { [k: string]: any }): (string|null);

        /**
         * Creates a GS2CEnterRoomRet message from a plain object. Also converts values to their respective internal types.
         * @param object Plain object
         * @returns GS2CEnterRoomRet
         */
        public static fromObject(object: { [k: string]: any }): pb.GS2CEnterRoomRet;

        /**
         * Creates a plain object from a GS2CEnterRoomRet message. Also converts values to other types if specified.
         * @param message GS2CEnterRoomRet
         * @param [options] Conversion options
         * @returns Plain object
         */
        public static toObject(message: pb.GS2CEnterRoomRet, options?: $protobuf.IConversionOptions): { [k: string]: any };

        /**
         * Converts this GS2CEnterRoomRet to JSON.
         * @returns JSON object
         */
        public toJSON(): { [k: string]: any };
    }

    namespace GS2CEnterRoomRet {

        /** ErrorCode enum. */
        enum ErrorCode {
            Success = 1,
            Fail = 2,
            GameStart = 3,
            NeedPassword = 4
        }
    }
}
