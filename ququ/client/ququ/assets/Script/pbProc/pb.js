/*eslint-disable block-scoped-var, id-length, no-control-regex, no-magic-numbers, no-prototype-builtins, no-redeclare, no-shadow, no-var, sort-vars*/
"use strict";

var $protobuf = require("protobufjs/minimal");

// Common aliases
var $Reader = $protobuf.Reader, $Writer = $protobuf.Writer, $util = $protobuf.util;

// Exported root namespace
var $root = $protobuf.roots["default"] || ($protobuf.roots["default"] = {});

$root.pb = (function() {

    /**
     * Namespace pb.
     * @exports pb
     * @namespace
     */
    var pb = {};

    pb.PlayerInfo = (function() {

        /**
         * Properties of a PlayerInfo.
         * @memberof pb
         * @interface IPlayerInfo
         * @property {number} playerId PlayerInfo playerId
         * @property {string} nickname PlayerInfo nickname
         * @property {string} headicon PlayerInfo headicon
         * @property {number} card PlayerInfo card
         * @property {number} coin PlayerInfo coin
         */

        /**
         * Constructs a new PlayerInfo.
         * @memberof pb
         * @classdesc Represents a PlayerInfo.
         * @implements IPlayerInfo
         * @constructor
         * @param {pb.IPlayerInfo=} [properties] Properties to set
         */
        function PlayerInfo(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * PlayerInfo playerId.
         * @member {number} playerId
         * @memberof pb.PlayerInfo
         * @instance
         */
        PlayerInfo.prototype.playerId = 0;

        /**
         * PlayerInfo nickname.
         * @member {string} nickname
         * @memberof pb.PlayerInfo
         * @instance
         */
        PlayerInfo.prototype.nickname = "";

        /**
         * PlayerInfo headicon.
         * @member {string} headicon
         * @memberof pb.PlayerInfo
         * @instance
         */
        PlayerInfo.prototype.headicon = "";

        /**
         * PlayerInfo card.
         * @member {number} card
         * @memberof pb.PlayerInfo
         * @instance
         */
        PlayerInfo.prototype.card = 0;

        /**
         * PlayerInfo coin.
         * @member {number} coin
         * @memberof pb.PlayerInfo
         * @instance
         */
        PlayerInfo.prototype.coin = 0;

        /**
         * Creates a new PlayerInfo instance using the specified properties.
         * @function create
         * @memberof pb.PlayerInfo
         * @static
         * @param {pb.IPlayerInfo=} [properties] Properties to set
         * @returns {pb.PlayerInfo} PlayerInfo instance
         */
        PlayerInfo.create = function create(properties) {
            return new PlayerInfo(properties);
        };

        /**
         * Encodes the specified PlayerInfo message. Does not implicitly {@link pb.PlayerInfo.verify|verify} messages.
         * @function encode
         * @memberof pb.PlayerInfo
         * @static
         * @param {pb.IPlayerInfo} message PlayerInfo message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        PlayerInfo.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int32(message.playerId);
            writer.uint32(/* id 2, wireType 2 =*/18).string(message.nickname);
            writer.uint32(/* id 3, wireType 2 =*/26).string(message.headicon);
            writer.uint32(/* id 4, wireType 0 =*/32).int32(message.card);
            writer.uint32(/* id 5, wireType 0 =*/40).int32(message.coin);
            return writer;
        };

        /**
         * Encodes the specified PlayerInfo message, length delimited. Does not implicitly {@link pb.PlayerInfo.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.PlayerInfo
         * @static
         * @param {pb.IPlayerInfo} message PlayerInfo message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        PlayerInfo.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a PlayerInfo message from the specified reader or buffer.
         * @function decode
         * @memberof pb.PlayerInfo
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.PlayerInfo} PlayerInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        PlayerInfo.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.PlayerInfo();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.playerId = reader.int32();
                    break;
                case 2:
                    message.nickname = reader.string();
                    break;
                case 3:
                    message.headicon = reader.string();
                    break;
                case 4:
                    message.card = reader.int32();
                    break;
                case 5:
                    message.coin = reader.int32();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("playerId"))
                throw $util.ProtocolError("missing required 'playerId'", { instance: message });
            if (!message.hasOwnProperty("nickname"))
                throw $util.ProtocolError("missing required 'nickname'", { instance: message });
            if (!message.hasOwnProperty("headicon"))
                throw $util.ProtocolError("missing required 'headicon'", { instance: message });
            if (!message.hasOwnProperty("card"))
                throw $util.ProtocolError("missing required 'card'", { instance: message });
            if (!message.hasOwnProperty("coin"))
                throw $util.ProtocolError("missing required 'coin'", { instance: message });
            return message;
        };

        /**
         * Decodes a PlayerInfo message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.PlayerInfo
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.PlayerInfo} PlayerInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        PlayerInfo.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a PlayerInfo message.
         * @function verify
         * @memberof pb.PlayerInfo
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        PlayerInfo.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isInteger(message.playerId))
                return "playerId: integer expected";
            if (!$util.isString(message.nickname))
                return "nickname: string expected";
            if (!$util.isString(message.headicon))
                return "headicon: string expected";
            if (!$util.isInteger(message.card))
                return "card: integer expected";
            if (!$util.isInteger(message.coin))
                return "coin: integer expected";
            return null;
        };

        /**
         * Creates a PlayerInfo message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.PlayerInfo
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.PlayerInfo} PlayerInfo
         */
        PlayerInfo.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.PlayerInfo)
                return object;
            var message = new $root.pb.PlayerInfo();
            if (object.playerId != null)
                message.playerId = object.playerId | 0;
            if (object.nickname != null)
                message.nickname = String(object.nickname);
            if (object.headicon != null)
                message.headicon = String(object.headicon);
            if (object.card != null)
                message.card = object.card | 0;
            if (object.coin != null)
                message.coin = object.coin | 0;
            return message;
        };

        /**
         * Creates a plain object from a PlayerInfo message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.PlayerInfo
         * @static
         * @param {pb.PlayerInfo} message PlayerInfo
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        PlayerInfo.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                object.playerId = 0;
                object.nickname = "";
                object.headicon = "";
                object.card = 0;
                object.coin = 0;
            }
            if (message.playerId != null && message.hasOwnProperty("playerId"))
                object.playerId = message.playerId;
            if (message.nickname != null && message.hasOwnProperty("nickname"))
                object.nickname = message.nickname;
            if (message.headicon != null && message.hasOwnProperty("headicon"))
                object.headicon = message.headicon;
            if (message.card != null && message.hasOwnProperty("card"))
                object.card = message.card;
            if (message.coin != null && message.hasOwnProperty("coin"))
                object.coin = message.coin;
            return object;
        };

        /**
         * Converts this PlayerInfo to JSON.
         * @function toJSON
         * @memberof pb.PlayerInfo
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        PlayerInfo.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return PlayerInfo;
    })();

    pb.C2GSLogin = (function() {

        /**
         * Properties of a C2GSLogin.
         * @memberof pb
         * @interface IC2GSLogin
         * @property {string} user C2GSLogin user
         * @property {string} password C2GSLogin password
         */

        /**
         * Constructs a new C2GSLogin.
         * @memberof pb
         * @classdesc Represents a C2GSLogin.
         * @implements IC2GSLogin
         * @constructor
         * @param {pb.IC2GSLogin=} [properties] Properties to set
         */
        function C2GSLogin(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * C2GSLogin user.
         * @member {string} user
         * @memberof pb.C2GSLogin
         * @instance
         */
        C2GSLogin.prototype.user = "";

        /**
         * C2GSLogin password.
         * @member {string} password
         * @memberof pb.C2GSLogin
         * @instance
         */
        C2GSLogin.prototype.password = "";

        /**
         * Creates a new C2GSLogin instance using the specified properties.
         * @function create
         * @memberof pb.C2GSLogin
         * @static
         * @param {pb.IC2GSLogin=} [properties] Properties to set
         * @returns {pb.C2GSLogin} C2GSLogin instance
         */
        C2GSLogin.create = function create(properties) {
            return new C2GSLogin(properties);
        };

        /**
         * Encodes the specified C2GSLogin message. Does not implicitly {@link pb.C2GSLogin.verify|verify} messages.
         * @function encode
         * @memberof pb.C2GSLogin
         * @static
         * @param {pb.IC2GSLogin} message C2GSLogin message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSLogin.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 2 =*/10).string(message.user);
            writer.uint32(/* id 2, wireType 2 =*/18).string(message.password);
            return writer;
        };

        /**
         * Encodes the specified C2GSLogin message, length delimited. Does not implicitly {@link pb.C2GSLogin.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.C2GSLogin
         * @static
         * @param {pb.IC2GSLogin} message C2GSLogin message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSLogin.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a C2GSLogin message from the specified reader or buffer.
         * @function decode
         * @memberof pb.C2GSLogin
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.C2GSLogin} C2GSLogin
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSLogin.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.C2GSLogin();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.user = reader.string();
                    break;
                case 2:
                    message.password = reader.string();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("user"))
                throw $util.ProtocolError("missing required 'user'", { instance: message });
            if (!message.hasOwnProperty("password"))
                throw $util.ProtocolError("missing required 'password'", { instance: message });
            return message;
        };

        /**
         * Decodes a C2GSLogin message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.C2GSLogin
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.C2GSLogin} C2GSLogin
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSLogin.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a C2GSLogin message.
         * @function verify
         * @memberof pb.C2GSLogin
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        C2GSLogin.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isString(message.user))
                return "user: string expected";
            if (!$util.isString(message.password))
                return "password: string expected";
            return null;
        };

        /**
         * Creates a C2GSLogin message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.C2GSLogin
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.C2GSLogin} C2GSLogin
         */
        C2GSLogin.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.C2GSLogin)
                return object;
            var message = new $root.pb.C2GSLogin();
            if (object.user != null)
                message.user = String(object.user);
            if (object.password != null)
                message.password = String(object.password);
            return message;
        };

        /**
         * Creates a plain object from a C2GSLogin message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.C2GSLogin
         * @static
         * @param {pb.C2GSLogin} message C2GSLogin
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        C2GSLogin.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                object.user = "";
                object.password = "";
            }
            if (message.user != null && message.hasOwnProperty("user"))
                object.user = message.user;
            if (message.password != null && message.hasOwnProperty("password"))
                object.password = message.password;
            return object;
        };

        /**
         * Converts this C2GSLogin to JSON.
         * @function toJSON
         * @memberof pb.C2GSLogin
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        C2GSLogin.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return C2GSLogin;
    })();

    pb.GS2CLoginRet = (function() {

        /**
         * Properties of a GS2CLoginRet.
         * @memberof pb
         * @interface IGS2CLoginRet
         * @property {pb.IPlayerInfo} user GS2CLoginRet user
         * @property {pb.GS2CLoginRet.ErrorCode} errorCode GS2CLoginRet errorCode
         */

        /**
         * Constructs a new GS2CLoginRet.
         * @memberof pb
         * @classdesc Represents a GS2CLoginRet.
         * @implements IGS2CLoginRet
         * @constructor
         * @param {pb.IGS2CLoginRet=} [properties] Properties to set
         */
        function GS2CLoginRet(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CLoginRet user.
         * @member {pb.IPlayerInfo} user
         * @memberof pb.GS2CLoginRet
         * @instance
         */
        GS2CLoginRet.prototype.user = null;

        /**
         * GS2CLoginRet errorCode.
         * @member {pb.GS2CLoginRet.ErrorCode} errorCode
         * @memberof pb.GS2CLoginRet
         * @instance
         */
        GS2CLoginRet.prototype.errorCode = 1;

        /**
         * Creates a new GS2CLoginRet instance using the specified properties.
         * @function create
         * @memberof pb.GS2CLoginRet
         * @static
         * @param {pb.IGS2CLoginRet=} [properties] Properties to set
         * @returns {pb.GS2CLoginRet} GS2CLoginRet instance
         */
        GS2CLoginRet.create = function create(properties) {
            return new GS2CLoginRet(properties);
        };

        /**
         * Encodes the specified GS2CLoginRet message. Does not implicitly {@link pb.GS2CLoginRet.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CLoginRet
         * @static
         * @param {pb.IGS2CLoginRet} message GS2CLoginRet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CLoginRet.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            $root.pb.PlayerInfo.encode(message.user, writer.uint32(/* id 1, wireType 2 =*/10).fork()).ldelim();
            writer.uint32(/* id 2, wireType 0 =*/16).int32(message.errorCode);
            return writer;
        };

        /**
         * Encodes the specified GS2CLoginRet message, length delimited. Does not implicitly {@link pb.GS2CLoginRet.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CLoginRet
         * @static
         * @param {pb.IGS2CLoginRet} message GS2CLoginRet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CLoginRet.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CLoginRet message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CLoginRet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CLoginRet} GS2CLoginRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CLoginRet.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CLoginRet();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.user = $root.pb.PlayerInfo.decode(reader, reader.uint32());
                    break;
                case 2:
                    message.errorCode = reader.int32();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("user"))
                throw $util.ProtocolError("missing required 'user'", { instance: message });
            if (!message.hasOwnProperty("errorCode"))
                throw $util.ProtocolError("missing required 'errorCode'", { instance: message });
            return message;
        };

        /**
         * Decodes a GS2CLoginRet message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CLoginRet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CLoginRet} GS2CLoginRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CLoginRet.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CLoginRet message.
         * @function verify
         * @memberof pb.GS2CLoginRet
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CLoginRet.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            {
                var error = $root.pb.PlayerInfo.verify(message.user);
                if (error)
                    return "user." + error;
            }
            switch (message.errorCode) {
            default:
                return "errorCode: enum value expected";
            case 1:
            case 2:
                break;
            }
            return null;
        };

        /**
         * Creates a GS2CLoginRet message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CLoginRet
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CLoginRet} GS2CLoginRet
         */
        GS2CLoginRet.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CLoginRet)
                return object;
            var message = new $root.pb.GS2CLoginRet();
            if (object.user != null) {
                if (typeof object.user !== "object")
                    throw TypeError(".pb.GS2CLoginRet.user: object expected");
                message.user = $root.pb.PlayerInfo.fromObject(object.user);
            }
            switch (object.errorCode) {
            case "Success":
            case 1:
                message.errorCode = 1;
                break;
            case "Fail":
            case 2:
                message.errorCode = 2;
                break;
            }
            return message;
        };

        /**
         * Creates a plain object from a GS2CLoginRet message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CLoginRet
         * @static
         * @param {pb.GS2CLoginRet} message GS2CLoginRet
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CLoginRet.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                object.user = null;
                object.errorCode = options.enums === String ? "Success" : 1;
            }
            if (message.user != null && message.hasOwnProperty("user"))
                object.user = $root.pb.PlayerInfo.toObject(message.user, options);
            if (message.errorCode != null && message.hasOwnProperty("errorCode"))
                object.errorCode = options.enums === String ? $root.pb.GS2CLoginRet.ErrorCode[message.errorCode] : message.errorCode;
            return object;
        };

        /**
         * Converts this GS2CLoginRet to JSON.
         * @function toJSON
         * @memberof pb.GS2CLoginRet
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CLoginRet.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        /**
         * ErrorCode enum.
         * @name pb.GS2CLoginRet.ErrorCode
         * @enum {string}
         * @property {number} Success=1 Success value
         * @property {number} Fail=2 Fail value
         */
        GS2CLoginRet.ErrorCode = (function() {
            var valuesById = {}, values = Object.create(valuesById);
            values[valuesById[1] = "Success"] = 1;
            values[valuesById[2] = "Fail"] = 2;
            return values;
        })();

        return GS2CLoginRet;
    })();

    return pb;
})();

module.exports = $root;
