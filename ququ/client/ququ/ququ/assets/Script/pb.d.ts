import * as $protobuf from "protobufjs";
/** Namespace pb. */
export namespace pb {

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
        user: string;

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
        public user: string;

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
}
