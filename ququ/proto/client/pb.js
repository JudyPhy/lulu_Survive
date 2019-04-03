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

    /**
     * PayMode enum.
     * @name pb.PayMode
     * @enum {string}
     * @property {number} SHARE=0 SHARE value
     * @property {number} OWNER=1 OWNER value
     */
    pb.PayMode = (function() {
        var valuesById = {}, values = Object.create(valuesById);
        values[valuesById[0] = "SHARE"] = 0;
        values[valuesById[1] = "OWNER"] = 1;
        return values;
    })();

    /**
     * GameRound enum.
     * @name pb.GameRound
     * @enum {string}
     * @property {number} ROUND4=0 ROUND4 value
     * @property {number} ROUND8=1 ROUND8 value
     */
    pb.GameRound = (function() {
        var valuesById = {}, values = Object.create(valuesById);
        values[valuesById[0] = "ROUND4"] = 0;
        values[valuesById[1] = "ROUND8"] = 1;
        return values;
    })();

    /**
     * Side enum.
     * @name pb.Side
     * @enum {string}
     * @property {number} BLUE=0 BLUE value
     * @property {number} RED=1 RED value
     */
    pb.Side = (function() {
        var valuesById = {}, values = Object.create(valuesById);
        values[valuesById[0] = "BLUE"] = 0;
        values[valuesById[1] = "RED"] = 1;
        return values;
    })();

    pb.PlayerInfo = (function() {

        /**
         * Properties of a PlayerInfo.
         * @memberof pb
         * @interface IPlayerInfo
         * @property {number|Long} playerId PlayerInfo playerId
         * @property {string} nickname PlayerInfo nickname
         * @property {string} headicon PlayerInfo headicon
         * @property {number|Long} card PlayerInfo card
         * @property {number|Long} coin PlayerInfo coin
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
         * @member {number|Long} playerId
         * @memberof pb.PlayerInfo
         * @instance
         */
        PlayerInfo.prototype.playerId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

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
         * @member {number|Long} card
         * @memberof pb.PlayerInfo
         * @instance
         */
        PlayerInfo.prototype.card = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * PlayerInfo coin.
         * @member {number|Long} coin
         * @memberof pb.PlayerInfo
         * @instance
         */
        PlayerInfo.prototype.coin = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

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
            writer.uint32(/* id 1, wireType 0 =*/8).int64(message.playerId);
            writer.uint32(/* id 2, wireType 2 =*/18).string(message.nickname);
            writer.uint32(/* id 3, wireType 2 =*/26).string(message.headicon);
            writer.uint32(/* id 4, wireType 0 =*/32).int64(message.card);
            writer.uint32(/* id 5, wireType 0 =*/40).int64(message.coin);
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
                    message.playerId = reader.int64();
                    break;
                case 2:
                    message.nickname = reader.string();
                    break;
                case 3:
                    message.headicon = reader.string();
                    break;
                case 4:
                    message.card = reader.int64();
                    break;
                case 5:
                    message.coin = reader.int64();
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
            if (!$util.isInteger(message.playerId) && !(message.playerId && $util.isInteger(message.playerId.low) && $util.isInteger(message.playerId.high)))
                return "playerId: integer|Long expected";
            if (!$util.isString(message.nickname))
                return "nickname: string expected";
            if (!$util.isString(message.headicon))
                return "headicon: string expected";
            if (!$util.isInteger(message.card) && !(message.card && $util.isInteger(message.card.low) && $util.isInteger(message.card.high)))
                return "card: integer|Long expected";
            if (!$util.isInteger(message.coin) && !(message.coin && $util.isInteger(message.coin.low) && $util.isInteger(message.coin.high)))
                return "coin: integer|Long expected";
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
                if ($util.Long)
                    (message.playerId = $util.Long.fromValue(object.playerId)).unsigned = false;
                else if (typeof object.playerId === "string")
                    message.playerId = parseInt(object.playerId, 10);
                else if (typeof object.playerId === "number")
                    message.playerId = object.playerId;
                else if (typeof object.playerId === "object")
                    message.playerId = new $util.LongBits(object.playerId.low >>> 0, object.playerId.high >>> 0).toNumber();
            if (object.nickname != null)
                message.nickname = String(object.nickname);
            if (object.headicon != null)
                message.headicon = String(object.headicon);
            if (object.card != null)
                if ($util.Long)
                    (message.card = $util.Long.fromValue(object.card)).unsigned = false;
                else if (typeof object.card === "string")
                    message.card = parseInt(object.card, 10);
                else if (typeof object.card === "number")
                    message.card = object.card;
                else if (typeof object.card === "object")
                    message.card = new $util.LongBits(object.card.low >>> 0, object.card.high >>> 0).toNumber();
            if (object.coin != null)
                if ($util.Long)
                    (message.coin = $util.Long.fromValue(object.coin)).unsigned = false;
                else if (typeof object.coin === "string")
                    message.coin = parseInt(object.coin, 10);
                else if (typeof object.coin === "number")
                    message.coin = object.coin;
                else if (typeof object.coin === "object")
                    message.coin = new $util.LongBits(object.coin.low >>> 0, object.coin.high >>> 0).toNumber();
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
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.playerId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.playerId = options.longs === String ? "0" : 0;
                object.nickname = "";
                object.headicon = "";
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.card = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.card = options.longs === String ? "0" : 0;
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.coin = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.coin = options.longs === String ? "0" : 0;
            }
            if (message.playerId != null && message.hasOwnProperty("playerId"))
                if (typeof message.playerId === "number")
                    object.playerId = options.longs === String ? String(message.playerId) : message.playerId;
                else
                    object.playerId = options.longs === String ? $util.Long.prototype.toString.call(message.playerId) : options.longs === Number ? new $util.LongBits(message.playerId.low >>> 0, message.playerId.high >>> 0).toNumber() : message.playerId;
            if (message.nickname != null && message.hasOwnProperty("nickname"))
                object.nickname = message.nickname;
            if (message.headicon != null && message.hasOwnProperty("headicon"))
                object.headicon = message.headicon;
            if (message.card != null && message.hasOwnProperty("card"))
                if (typeof message.card === "number")
                    object.card = options.longs === String ? String(message.card) : message.card;
                else
                    object.card = options.longs === String ? $util.Long.prototype.toString.call(message.card) : options.longs === Number ? new $util.LongBits(message.card.low >>> 0, message.card.high >>> 0).toNumber() : message.card;
            if (message.coin != null && message.hasOwnProperty("coin"))
                if (typeof message.coin === "number")
                    object.coin = options.longs === String ? String(message.coin) : message.coin;
                else
                    object.coin = options.longs === String ? $util.Long.prototype.toString.call(message.coin) : options.longs === Number ? new $util.LongBits(message.coin.low >>> 0, message.coin.high >>> 0).toNumber() : message.coin;
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

    pb.BetInfo = (function() {

        /**
         * Properties of a BetInfo.
         * @memberof pb
         * @interface IBetInfo
         * @property {number|Long} playerId BetInfo playerId
         * @property {pb.Side} betSide BetInfo betSide
         * @property {number|Long} betValue BetInfo betValue
         */

        /**
         * Constructs a new BetInfo.
         * @memberof pb
         * @classdesc Represents a BetInfo.
         * @implements IBetInfo
         * @constructor
         * @param {pb.IBetInfo=} [properties] Properties to set
         */
        function BetInfo(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * BetInfo playerId.
         * @member {number|Long} playerId
         * @memberof pb.BetInfo
         * @instance
         */
        BetInfo.prototype.playerId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * BetInfo betSide.
         * @member {pb.Side} betSide
         * @memberof pb.BetInfo
         * @instance
         */
        BetInfo.prototype.betSide = 0;

        /**
         * BetInfo betValue.
         * @member {number|Long} betValue
         * @memberof pb.BetInfo
         * @instance
         */
        BetInfo.prototype.betValue = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * Creates a new BetInfo instance using the specified properties.
         * @function create
         * @memberof pb.BetInfo
         * @static
         * @param {pb.IBetInfo=} [properties] Properties to set
         * @returns {pb.BetInfo} BetInfo instance
         */
        BetInfo.create = function create(properties) {
            return new BetInfo(properties);
        };

        /**
         * Encodes the specified BetInfo message. Does not implicitly {@link pb.BetInfo.verify|verify} messages.
         * @function encode
         * @memberof pb.BetInfo
         * @static
         * @param {pb.IBetInfo} message BetInfo message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        BetInfo.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int64(message.playerId);
            writer.uint32(/* id 2, wireType 0 =*/16).int32(message.betSide);
            writer.uint32(/* id 3, wireType 0 =*/24).int64(message.betValue);
            return writer;
        };

        /**
         * Encodes the specified BetInfo message, length delimited. Does not implicitly {@link pb.BetInfo.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.BetInfo
         * @static
         * @param {pb.IBetInfo} message BetInfo message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        BetInfo.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a BetInfo message from the specified reader or buffer.
         * @function decode
         * @memberof pb.BetInfo
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.BetInfo} BetInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        BetInfo.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.BetInfo();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.playerId = reader.int64();
                    break;
                case 2:
                    message.betSide = reader.int32();
                    break;
                case 3:
                    message.betValue = reader.int64();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("playerId"))
                throw $util.ProtocolError("missing required 'playerId'", { instance: message });
            if (!message.hasOwnProperty("betSide"))
                throw $util.ProtocolError("missing required 'betSide'", { instance: message });
            if (!message.hasOwnProperty("betValue"))
                throw $util.ProtocolError("missing required 'betValue'", { instance: message });
            return message;
        };

        /**
         * Decodes a BetInfo message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.BetInfo
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.BetInfo} BetInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        BetInfo.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a BetInfo message.
         * @function verify
         * @memberof pb.BetInfo
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        BetInfo.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isInteger(message.playerId) && !(message.playerId && $util.isInteger(message.playerId.low) && $util.isInteger(message.playerId.high)))
                return "playerId: integer|Long expected";
            switch (message.betSide) {
            default:
                return "betSide: enum value expected";
            case 0:
            case 1:
                break;
            }
            if (!$util.isInteger(message.betValue) && !(message.betValue && $util.isInteger(message.betValue.low) && $util.isInteger(message.betValue.high)))
                return "betValue: integer|Long expected";
            return null;
        };

        /**
         * Creates a BetInfo message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.BetInfo
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.BetInfo} BetInfo
         */
        BetInfo.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.BetInfo)
                return object;
            var message = new $root.pb.BetInfo();
            if (object.playerId != null)
                if ($util.Long)
                    (message.playerId = $util.Long.fromValue(object.playerId)).unsigned = false;
                else if (typeof object.playerId === "string")
                    message.playerId = parseInt(object.playerId, 10);
                else if (typeof object.playerId === "number")
                    message.playerId = object.playerId;
                else if (typeof object.playerId === "object")
                    message.playerId = new $util.LongBits(object.playerId.low >>> 0, object.playerId.high >>> 0).toNumber();
            switch (object.betSide) {
            case "BLUE":
            case 0:
                message.betSide = 0;
                break;
            case "RED":
            case 1:
                message.betSide = 1;
                break;
            }
            if (object.betValue != null)
                if ($util.Long)
                    (message.betValue = $util.Long.fromValue(object.betValue)).unsigned = false;
                else if (typeof object.betValue === "string")
                    message.betValue = parseInt(object.betValue, 10);
                else if (typeof object.betValue === "number")
                    message.betValue = object.betValue;
                else if (typeof object.betValue === "object")
                    message.betValue = new $util.LongBits(object.betValue.low >>> 0, object.betValue.high >>> 0).toNumber();
            return message;
        };

        /**
         * Creates a plain object from a BetInfo message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.BetInfo
         * @static
         * @param {pb.BetInfo} message BetInfo
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        BetInfo.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.playerId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.playerId = options.longs === String ? "0" : 0;
                object.betSide = options.enums === String ? "BLUE" : 0;
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.betValue = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.betValue = options.longs === String ? "0" : 0;
            }
            if (message.playerId != null && message.hasOwnProperty("playerId"))
                if (typeof message.playerId === "number")
                    object.playerId = options.longs === String ? String(message.playerId) : message.playerId;
                else
                    object.playerId = options.longs === String ? $util.Long.prototype.toString.call(message.playerId) : options.longs === Number ? new $util.LongBits(message.playerId.low >>> 0, message.playerId.high >>> 0).toNumber() : message.playerId;
            if (message.betSide != null && message.hasOwnProperty("betSide"))
                object.betSide = options.enums === String ? $root.pb.Side[message.betSide] : message.betSide;
            if (message.betValue != null && message.hasOwnProperty("betValue"))
                if (typeof message.betValue === "number")
                    object.betValue = options.longs === String ? String(message.betValue) : message.betValue;
                else
                    object.betValue = options.longs === String ? $util.Long.prototype.toString.call(message.betValue) : options.longs === Number ? new $util.LongBits(message.betValue.low >>> 0, message.betValue.high >>> 0).toNumber() : message.betValue;
            return object;
        };

        /**
         * Converts this BetInfo to JSON.
         * @function toJSON
         * @memberof pb.BetInfo
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        BetInfo.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return BetInfo;
    })();

    pb.RoomInfo = (function() {

        /**
         * Properties of a RoomInfo.
         * @memberof pb
         * @interface IRoomInfo
         * @property {number|Long} roomId RoomInfo roomId
         * @property {string} password RoomInfo password
         * @property {pb.PayMode} pay RoomInfo pay
         * @property {pb.GameRound} round RoomInfo round
         * @property {boolean} exitPay RoomInfo exitPay
         */

        /**
         * Constructs a new RoomInfo.
         * @memberof pb
         * @classdesc Represents a RoomInfo.
         * @implements IRoomInfo
         * @constructor
         * @param {pb.IRoomInfo=} [properties] Properties to set
         */
        function RoomInfo(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * RoomInfo roomId.
         * @member {number|Long} roomId
         * @memberof pb.RoomInfo
         * @instance
         */
        RoomInfo.prototype.roomId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * RoomInfo password.
         * @member {string} password
         * @memberof pb.RoomInfo
         * @instance
         */
        RoomInfo.prototype.password = "";

        /**
         * RoomInfo pay.
         * @member {pb.PayMode} pay
         * @memberof pb.RoomInfo
         * @instance
         */
        RoomInfo.prototype.pay = 0;

        /**
         * RoomInfo round.
         * @member {pb.GameRound} round
         * @memberof pb.RoomInfo
         * @instance
         */
        RoomInfo.prototype.round = 0;

        /**
         * RoomInfo exitPay.
         * @member {boolean} exitPay
         * @memberof pb.RoomInfo
         * @instance
         */
        RoomInfo.prototype.exitPay = false;

        /**
         * Creates a new RoomInfo instance using the specified properties.
         * @function create
         * @memberof pb.RoomInfo
         * @static
         * @param {pb.IRoomInfo=} [properties] Properties to set
         * @returns {pb.RoomInfo} RoomInfo instance
         */
        RoomInfo.create = function create(properties) {
            return new RoomInfo(properties);
        };

        /**
         * Encodes the specified RoomInfo message. Does not implicitly {@link pb.RoomInfo.verify|verify} messages.
         * @function encode
         * @memberof pb.RoomInfo
         * @static
         * @param {pb.IRoomInfo} message RoomInfo message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        RoomInfo.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int64(message.roomId);
            writer.uint32(/* id 2, wireType 2 =*/18).string(message.password);
            writer.uint32(/* id 3, wireType 0 =*/24).int32(message.pay);
            writer.uint32(/* id 4, wireType 0 =*/32).int32(message.round);
            writer.uint32(/* id 5, wireType 0 =*/40).bool(message.exitPay);
            return writer;
        };

        /**
         * Encodes the specified RoomInfo message, length delimited. Does not implicitly {@link pb.RoomInfo.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.RoomInfo
         * @static
         * @param {pb.IRoomInfo} message RoomInfo message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        RoomInfo.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a RoomInfo message from the specified reader or buffer.
         * @function decode
         * @memberof pb.RoomInfo
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.RoomInfo} RoomInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        RoomInfo.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.RoomInfo();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.roomId = reader.int64();
                    break;
                case 2:
                    message.password = reader.string();
                    break;
                case 3:
                    message.pay = reader.int32();
                    break;
                case 4:
                    message.round = reader.int32();
                    break;
                case 5:
                    message.exitPay = reader.bool();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("roomId"))
                throw $util.ProtocolError("missing required 'roomId'", { instance: message });
            if (!message.hasOwnProperty("password"))
                throw $util.ProtocolError("missing required 'password'", { instance: message });
            if (!message.hasOwnProperty("pay"))
                throw $util.ProtocolError("missing required 'pay'", { instance: message });
            if (!message.hasOwnProperty("round"))
                throw $util.ProtocolError("missing required 'round'", { instance: message });
            if (!message.hasOwnProperty("exitPay"))
                throw $util.ProtocolError("missing required 'exitPay'", { instance: message });
            return message;
        };

        /**
         * Decodes a RoomInfo message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.RoomInfo
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.RoomInfo} RoomInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        RoomInfo.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a RoomInfo message.
         * @function verify
         * @memberof pb.RoomInfo
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        RoomInfo.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isInteger(message.roomId) && !(message.roomId && $util.isInteger(message.roomId.low) && $util.isInteger(message.roomId.high)))
                return "roomId: integer|Long expected";
            if (!$util.isString(message.password))
                return "password: string expected";
            switch (message.pay) {
            default:
                return "pay: enum value expected";
            case 0:
            case 1:
                break;
            }
            switch (message.round) {
            default:
                return "round: enum value expected";
            case 0:
            case 1:
                break;
            }
            if (typeof message.exitPay !== "boolean")
                return "exitPay: boolean expected";
            return null;
        };

        /**
         * Creates a RoomInfo message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.RoomInfo
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.RoomInfo} RoomInfo
         */
        RoomInfo.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.RoomInfo)
                return object;
            var message = new $root.pb.RoomInfo();
            if (object.roomId != null)
                if ($util.Long)
                    (message.roomId = $util.Long.fromValue(object.roomId)).unsigned = false;
                else if (typeof object.roomId === "string")
                    message.roomId = parseInt(object.roomId, 10);
                else if (typeof object.roomId === "number")
                    message.roomId = object.roomId;
                else if (typeof object.roomId === "object")
                    message.roomId = new $util.LongBits(object.roomId.low >>> 0, object.roomId.high >>> 0).toNumber();
            if (object.password != null)
                message.password = String(object.password);
            switch (object.pay) {
            case "SHARE":
            case 0:
                message.pay = 0;
                break;
            case "OWNER":
            case 1:
                message.pay = 1;
                break;
            }
            switch (object.round) {
            case "ROUND4":
            case 0:
                message.round = 0;
                break;
            case "ROUND8":
            case 1:
                message.round = 1;
                break;
            }
            if (object.exitPay != null)
                message.exitPay = Boolean(object.exitPay);
            return message;
        };

        /**
         * Creates a plain object from a RoomInfo message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.RoomInfo
         * @static
         * @param {pb.RoomInfo} message RoomInfo
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        RoomInfo.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.roomId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.roomId = options.longs === String ? "0" : 0;
                object.password = "";
                object.pay = options.enums === String ? "SHARE" : 0;
                object.round = options.enums === String ? "ROUND4" : 0;
                object.exitPay = false;
            }
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (typeof message.roomId === "number")
                    object.roomId = options.longs === String ? String(message.roomId) : message.roomId;
                else
                    object.roomId = options.longs === String ? $util.Long.prototype.toString.call(message.roomId) : options.longs === Number ? new $util.LongBits(message.roomId.low >>> 0, message.roomId.high >>> 0).toNumber() : message.roomId;
            if (message.password != null && message.hasOwnProperty("password"))
                object.password = message.password;
            if (message.pay != null && message.hasOwnProperty("pay"))
                object.pay = options.enums === String ? $root.pb.PayMode[message.pay] : message.pay;
            if (message.round != null && message.hasOwnProperty("round"))
                object.round = options.enums === String ? $root.pb.GameRound[message.round] : message.round;
            if (message.exitPay != null && message.hasOwnProperty("exitPay"))
                object.exitPay = message.exitPay;
            return object;
        };

        /**
         * Converts this RoomInfo to JSON.
         * @function toJSON
         * @memberof pb.RoomInfo
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        RoomInfo.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return RoomInfo;
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

    pb.C2GSCreateRoom = (function() {

        /**
         * Properties of a C2GSCreateRoom.
         * @memberof pb
         * @interface IC2GSCreateRoom
         * @property {pb.PayMode} pay C2GSCreateRoom pay
         * @property {pb.GameRound} round C2GSCreateRoom round
         * @property {boolean} exitPay C2GSCreateRoom exitPay
         * @property {number|Long} password C2GSCreateRoom password
         */

        /**
         * Constructs a new C2GSCreateRoom.
         * @memberof pb
         * @classdesc Represents a C2GSCreateRoom.
         * @implements IC2GSCreateRoom
         * @constructor
         * @param {pb.IC2GSCreateRoom=} [properties] Properties to set
         */
        function C2GSCreateRoom(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * C2GSCreateRoom pay.
         * @member {pb.PayMode} pay
         * @memberof pb.C2GSCreateRoom
         * @instance
         */
        C2GSCreateRoom.prototype.pay = 0;

        /**
         * C2GSCreateRoom round.
         * @member {pb.GameRound} round
         * @memberof pb.C2GSCreateRoom
         * @instance
         */
        C2GSCreateRoom.prototype.round = 0;

        /**
         * C2GSCreateRoom exitPay.
         * @member {boolean} exitPay
         * @memberof pb.C2GSCreateRoom
         * @instance
         */
        C2GSCreateRoom.prototype.exitPay = false;

        /**
         * C2GSCreateRoom password.
         * @member {number|Long} password
         * @memberof pb.C2GSCreateRoom
         * @instance
         */
        C2GSCreateRoom.prototype.password = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * Creates a new C2GSCreateRoom instance using the specified properties.
         * @function create
         * @memberof pb.C2GSCreateRoom
         * @static
         * @param {pb.IC2GSCreateRoom=} [properties] Properties to set
         * @returns {pb.C2GSCreateRoom} C2GSCreateRoom instance
         */
        C2GSCreateRoom.create = function create(properties) {
            return new C2GSCreateRoom(properties);
        };

        /**
         * Encodes the specified C2GSCreateRoom message. Does not implicitly {@link pb.C2GSCreateRoom.verify|verify} messages.
         * @function encode
         * @memberof pb.C2GSCreateRoom
         * @static
         * @param {pb.IC2GSCreateRoom} message C2GSCreateRoom message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSCreateRoom.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int32(message.pay);
            writer.uint32(/* id 2, wireType 0 =*/16).int32(message.round);
            writer.uint32(/* id 3, wireType 0 =*/24).bool(message.exitPay);
            writer.uint32(/* id 4, wireType 0 =*/32).int64(message.password);
            return writer;
        };

        /**
         * Encodes the specified C2GSCreateRoom message, length delimited. Does not implicitly {@link pb.C2GSCreateRoom.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.C2GSCreateRoom
         * @static
         * @param {pb.IC2GSCreateRoom} message C2GSCreateRoom message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSCreateRoom.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a C2GSCreateRoom message from the specified reader or buffer.
         * @function decode
         * @memberof pb.C2GSCreateRoom
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.C2GSCreateRoom} C2GSCreateRoom
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSCreateRoom.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.C2GSCreateRoom();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.pay = reader.int32();
                    break;
                case 2:
                    message.round = reader.int32();
                    break;
                case 3:
                    message.exitPay = reader.bool();
                    break;
                case 4:
                    message.password = reader.int64();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("pay"))
                throw $util.ProtocolError("missing required 'pay'", { instance: message });
            if (!message.hasOwnProperty("round"))
                throw $util.ProtocolError("missing required 'round'", { instance: message });
            if (!message.hasOwnProperty("exitPay"))
                throw $util.ProtocolError("missing required 'exitPay'", { instance: message });
            if (!message.hasOwnProperty("password"))
                throw $util.ProtocolError("missing required 'password'", { instance: message });
            return message;
        };

        /**
         * Decodes a C2GSCreateRoom message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.C2GSCreateRoom
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.C2GSCreateRoom} C2GSCreateRoom
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSCreateRoom.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a C2GSCreateRoom message.
         * @function verify
         * @memberof pb.C2GSCreateRoom
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        C2GSCreateRoom.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            switch (message.pay) {
            default:
                return "pay: enum value expected";
            case 0:
            case 1:
                break;
            }
            switch (message.round) {
            default:
                return "round: enum value expected";
            case 0:
            case 1:
                break;
            }
            if (typeof message.exitPay !== "boolean")
                return "exitPay: boolean expected";
            if (!$util.isInteger(message.password) && !(message.password && $util.isInteger(message.password.low) && $util.isInteger(message.password.high)))
                return "password: integer|Long expected";
            return null;
        };

        /**
         * Creates a C2GSCreateRoom message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.C2GSCreateRoom
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.C2GSCreateRoom} C2GSCreateRoom
         */
        C2GSCreateRoom.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.C2GSCreateRoom)
                return object;
            var message = new $root.pb.C2GSCreateRoom();
            switch (object.pay) {
            case "SHARE":
            case 0:
                message.pay = 0;
                break;
            case "OWNER":
            case 1:
                message.pay = 1;
                break;
            }
            switch (object.round) {
            case "ROUND4":
            case 0:
                message.round = 0;
                break;
            case "ROUND8":
            case 1:
                message.round = 1;
                break;
            }
            if (object.exitPay != null)
                message.exitPay = Boolean(object.exitPay);
            if (object.password != null)
                if ($util.Long)
                    (message.password = $util.Long.fromValue(object.password)).unsigned = false;
                else if (typeof object.password === "string")
                    message.password = parseInt(object.password, 10);
                else if (typeof object.password === "number")
                    message.password = object.password;
                else if (typeof object.password === "object")
                    message.password = new $util.LongBits(object.password.low >>> 0, object.password.high >>> 0).toNumber();
            return message;
        };

        /**
         * Creates a plain object from a C2GSCreateRoom message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.C2GSCreateRoom
         * @static
         * @param {pb.C2GSCreateRoom} message C2GSCreateRoom
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        C2GSCreateRoom.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                object.pay = options.enums === String ? "SHARE" : 0;
                object.round = options.enums === String ? "ROUND4" : 0;
                object.exitPay = false;
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.password = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.password = options.longs === String ? "0" : 0;
            }
            if (message.pay != null && message.hasOwnProperty("pay"))
                object.pay = options.enums === String ? $root.pb.PayMode[message.pay] : message.pay;
            if (message.round != null && message.hasOwnProperty("round"))
                object.round = options.enums === String ? $root.pb.GameRound[message.round] : message.round;
            if (message.exitPay != null && message.hasOwnProperty("exitPay"))
                object.exitPay = message.exitPay;
            if (message.password != null && message.hasOwnProperty("password"))
                if (typeof message.password === "number")
                    object.password = options.longs === String ? String(message.password) : message.password;
                else
                    object.password = options.longs === String ? $util.Long.prototype.toString.call(message.password) : options.longs === Number ? new $util.LongBits(message.password.low >>> 0, message.password.high >>> 0).toNumber() : message.password;
            return object;
        };

        /**
         * Converts this C2GSCreateRoom to JSON.
         * @function toJSON
         * @memberof pb.C2GSCreateRoom
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        C2GSCreateRoom.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return C2GSCreateRoom;
    })();

    pb.C2GSEnterRoom = (function() {

        /**
         * Properties of a C2GSEnterRoom.
         * @memberof pb
         * @interface IC2GSEnterRoom
         * @property {number|Long|null} [roomId] C2GSEnterRoom roomId
         * @property {number|Long|null} [password] C2GSEnterRoom password
         */

        /**
         * Constructs a new C2GSEnterRoom.
         * @memberof pb
         * @classdesc Represents a C2GSEnterRoom.
         * @implements IC2GSEnterRoom
         * @constructor
         * @param {pb.IC2GSEnterRoom=} [properties] Properties to set
         */
        function C2GSEnterRoom(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * C2GSEnterRoom roomId.
         * @member {number|Long} roomId
         * @memberof pb.C2GSEnterRoom
         * @instance
         */
        C2GSEnterRoom.prototype.roomId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * C2GSEnterRoom password.
         * @member {number|Long} password
         * @memberof pb.C2GSEnterRoom
         * @instance
         */
        C2GSEnterRoom.prototype.password = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * Creates a new C2GSEnterRoom instance using the specified properties.
         * @function create
         * @memberof pb.C2GSEnterRoom
         * @static
         * @param {pb.IC2GSEnterRoom=} [properties] Properties to set
         * @returns {pb.C2GSEnterRoom} C2GSEnterRoom instance
         */
        C2GSEnterRoom.create = function create(properties) {
            return new C2GSEnterRoom(properties);
        };

        /**
         * Encodes the specified C2GSEnterRoom message. Does not implicitly {@link pb.C2GSEnterRoom.verify|verify} messages.
         * @function encode
         * @memberof pb.C2GSEnterRoom
         * @static
         * @param {pb.IC2GSEnterRoom} message C2GSEnterRoom message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSEnterRoom.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                writer.uint32(/* id 1, wireType 0 =*/8).int64(message.roomId);
            if (message.password != null && message.hasOwnProperty("password"))
                writer.uint32(/* id 2, wireType 0 =*/16).int64(message.password);
            return writer;
        };

        /**
         * Encodes the specified C2GSEnterRoom message, length delimited. Does not implicitly {@link pb.C2GSEnterRoom.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.C2GSEnterRoom
         * @static
         * @param {pb.IC2GSEnterRoom} message C2GSEnterRoom message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSEnterRoom.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a C2GSEnterRoom message from the specified reader or buffer.
         * @function decode
         * @memberof pb.C2GSEnterRoom
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.C2GSEnterRoom} C2GSEnterRoom
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSEnterRoom.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.C2GSEnterRoom();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.roomId = reader.int64();
                    break;
                case 2:
                    message.password = reader.int64();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            return message;
        };

        /**
         * Decodes a C2GSEnterRoom message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.C2GSEnterRoom
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.C2GSEnterRoom} C2GSEnterRoom
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSEnterRoom.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a C2GSEnterRoom message.
         * @function verify
         * @memberof pb.C2GSEnterRoom
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        C2GSEnterRoom.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (!$util.isInteger(message.roomId) && !(message.roomId && $util.isInteger(message.roomId.low) && $util.isInteger(message.roomId.high)))
                    return "roomId: integer|Long expected";
            if (message.password != null && message.hasOwnProperty("password"))
                if (!$util.isInteger(message.password) && !(message.password && $util.isInteger(message.password.low) && $util.isInteger(message.password.high)))
                    return "password: integer|Long expected";
            return null;
        };

        /**
         * Creates a C2GSEnterRoom message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.C2GSEnterRoom
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.C2GSEnterRoom} C2GSEnterRoom
         */
        C2GSEnterRoom.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.C2GSEnterRoom)
                return object;
            var message = new $root.pb.C2GSEnterRoom();
            if (object.roomId != null)
                if ($util.Long)
                    (message.roomId = $util.Long.fromValue(object.roomId)).unsigned = false;
                else if (typeof object.roomId === "string")
                    message.roomId = parseInt(object.roomId, 10);
                else if (typeof object.roomId === "number")
                    message.roomId = object.roomId;
                else if (typeof object.roomId === "object")
                    message.roomId = new $util.LongBits(object.roomId.low >>> 0, object.roomId.high >>> 0).toNumber();
            if (object.password != null)
                if ($util.Long)
                    (message.password = $util.Long.fromValue(object.password)).unsigned = false;
                else if (typeof object.password === "string")
                    message.password = parseInt(object.password, 10);
                else if (typeof object.password === "number")
                    message.password = object.password;
                else if (typeof object.password === "object")
                    message.password = new $util.LongBits(object.password.low >>> 0, object.password.high >>> 0).toNumber();
            return message;
        };

        /**
         * Creates a plain object from a C2GSEnterRoom message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.C2GSEnterRoom
         * @static
         * @param {pb.C2GSEnterRoom} message C2GSEnterRoom
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        C2GSEnterRoom.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.roomId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.roomId = options.longs === String ? "0" : 0;
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.password = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.password = options.longs === String ? "0" : 0;
            }
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (typeof message.roomId === "number")
                    object.roomId = options.longs === String ? String(message.roomId) : message.roomId;
                else
                    object.roomId = options.longs === String ? $util.Long.prototype.toString.call(message.roomId) : options.longs === Number ? new $util.LongBits(message.roomId.low >>> 0, message.roomId.high >>> 0).toNumber() : message.roomId;
            if (message.password != null && message.hasOwnProperty("password"))
                if (typeof message.password === "number")
                    object.password = options.longs === String ? String(message.password) : message.password;
                else
                    object.password = options.longs === String ? $util.Long.prototype.toString.call(message.password) : options.longs === Number ? new $util.LongBits(message.password.low >>> 0, message.password.high >>> 0).toNumber() : message.password;
            return object;
        };

        /**
         * Converts this C2GSEnterRoom to JSON.
         * @function toJSON
         * @memberof pb.C2GSEnterRoom
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        C2GSEnterRoom.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return C2GSEnterRoom;
    })();

    pb.GS2CEnterRoomRet = (function() {

        /**
         * Properties of a GS2CEnterRoomRet.
         * @memberof pb
         * @interface IGS2CEnterRoomRet
         * @property {number|Long} roomId GS2CEnterRoomRet roomId
         * @property {number} rountIndex GS2CEnterRoomRet rountIndex
         * @property {Array.<pb.IPlayerInfo>|null} [players] GS2CEnterRoomRet players
         * @property {pb.GS2CEnterRoomRet.ErrorCode} errorCode GS2CEnterRoomRet errorCode
         */

        /**
         * Constructs a new GS2CEnterRoomRet.
         * @memberof pb
         * @classdesc Represents a GS2CEnterRoomRet.
         * @implements IGS2CEnterRoomRet
         * @constructor
         * @param {pb.IGS2CEnterRoomRet=} [properties] Properties to set
         */
        function GS2CEnterRoomRet(properties) {
            this.players = [];
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CEnterRoomRet roomId.
         * @member {number|Long} roomId
         * @memberof pb.GS2CEnterRoomRet
         * @instance
         */
        GS2CEnterRoomRet.prototype.roomId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * GS2CEnterRoomRet rountIndex.
         * @member {number} rountIndex
         * @memberof pb.GS2CEnterRoomRet
         * @instance
         */
        GS2CEnterRoomRet.prototype.rountIndex = 0;

        /**
         * GS2CEnterRoomRet players.
         * @member {Array.<pb.IPlayerInfo>} players
         * @memberof pb.GS2CEnterRoomRet
         * @instance
         */
        GS2CEnterRoomRet.prototype.players = $util.emptyArray;

        /**
         * GS2CEnterRoomRet errorCode.
         * @member {pb.GS2CEnterRoomRet.ErrorCode} errorCode
         * @memberof pb.GS2CEnterRoomRet
         * @instance
         */
        GS2CEnterRoomRet.prototype.errorCode = 1;

        /**
         * Creates a new GS2CEnterRoomRet instance using the specified properties.
         * @function create
         * @memberof pb.GS2CEnterRoomRet
         * @static
         * @param {pb.IGS2CEnterRoomRet=} [properties] Properties to set
         * @returns {pb.GS2CEnterRoomRet} GS2CEnterRoomRet instance
         */
        GS2CEnterRoomRet.create = function create(properties) {
            return new GS2CEnterRoomRet(properties);
        };

        /**
         * Encodes the specified GS2CEnterRoomRet message. Does not implicitly {@link pb.GS2CEnterRoomRet.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CEnterRoomRet
         * @static
         * @param {pb.IGS2CEnterRoomRet} message GS2CEnterRoomRet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CEnterRoomRet.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int64(message.roomId);
            writer.uint32(/* id 2, wireType 0 =*/16).int32(message.rountIndex);
            if (message.players != null && message.players.length)
                for (var i = 0; i < message.players.length; ++i)
                    $root.pb.PlayerInfo.encode(message.players[i], writer.uint32(/* id 3, wireType 2 =*/26).fork()).ldelim();
            writer.uint32(/* id 4, wireType 0 =*/32).int32(message.errorCode);
            return writer;
        };

        /**
         * Encodes the specified GS2CEnterRoomRet message, length delimited. Does not implicitly {@link pb.GS2CEnterRoomRet.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CEnterRoomRet
         * @static
         * @param {pb.IGS2CEnterRoomRet} message GS2CEnterRoomRet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CEnterRoomRet.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CEnterRoomRet message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CEnterRoomRet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CEnterRoomRet} GS2CEnterRoomRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CEnterRoomRet.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CEnterRoomRet();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.roomId = reader.int64();
                    break;
                case 2:
                    message.rountIndex = reader.int32();
                    break;
                case 3:
                    if (!(message.players && message.players.length))
                        message.players = [];
                    message.players.push($root.pb.PlayerInfo.decode(reader, reader.uint32()));
                    break;
                case 4:
                    message.errorCode = reader.int32();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("roomId"))
                throw $util.ProtocolError("missing required 'roomId'", { instance: message });
            if (!message.hasOwnProperty("rountIndex"))
                throw $util.ProtocolError("missing required 'rountIndex'", { instance: message });
            if (!message.hasOwnProperty("errorCode"))
                throw $util.ProtocolError("missing required 'errorCode'", { instance: message });
            return message;
        };

        /**
         * Decodes a GS2CEnterRoomRet message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CEnterRoomRet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CEnterRoomRet} GS2CEnterRoomRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CEnterRoomRet.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CEnterRoomRet message.
         * @function verify
         * @memberof pb.GS2CEnterRoomRet
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CEnterRoomRet.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isInteger(message.roomId) && !(message.roomId && $util.isInteger(message.roomId.low) && $util.isInteger(message.roomId.high)))
                return "roomId: integer|Long expected";
            if (!$util.isInteger(message.rountIndex))
                return "rountIndex: integer expected";
            if (message.players != null && message.hasOwnProperty("players")) {
                if (!Array.isArray(message.players))
                    return "players: array expected";
                for (var i = 0; i < message.players.length; ++i) {
                    var error = $root.pb.PlayerInfo.verify(message.players[i]);
                    if (error)
                        return "players." + error;
                }
            }
            switch (message.errorCode) {
            default:
                return "errorCode: enum value expected";
            case 1:
            case 2:
            case 3:
            case 4:
                break;
            }
            return null;
        };

        /**
         * Creates a GS2CEnterRoomRet message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CEnterRoomRet
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CEnterRoomRet} GS2CEnterRoomRet
         */
        GS2CEnterRoomRet.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CEnterRoomRet)
                return object;
            var message = new $root.pb.GS2CEnterRoomRet();
            if (object.roomId != null)
                if ($util.Long)
                    (message.roomId = $util.Long.fromValue(object.roomId)).unsigned = false;
                else if (typeof object.roomId === "string")
                    message.roomId = parseInt(object.roomId, 10);
                else if (typeof object.roomId === "number")
                    message.roomId = object.roomId;
                else if (typeof object.roomId === "object")
                    message.roomId = new $util.LongBits(object.roomId.low >>> 0, object.roomId.high >>> 0).toNumber();
            if (object.rountIndex != null)
                message.rountIndex = object.rountIndex | 0;
            if (object.players) {
                if (!Array.isArray(object.players))
                    throw TypeError(".pb.GS2CEnterRoomRet.players: array expected");
                message.players = [];
                for (var i = 0; i < object.players.length; ++i) {
                    if (typeof object.players[i] !== "object")
                        throw TypeError(".pb.GS2CEnterRoomRet.players: object expected");
                    message.players[i] = $root.pb.PlayerInfo.fromObject(object.players[i]);
                }
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
            case "GameStart":
            case 3:
                message.errorCode = 3;
                break;
            case "NeedPassword":
            case 4:
                message.errorCode = 4;
                break;
            }
            return message;
        };

        /**
         * Creates a plain object from a GS2CEnterRoomRet message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CEnterRoomRet
         * @static
         * @param {pb.GS2CEnterRoomRet} message GS2CEnterRoomRet
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CEnterRoomRet.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.arrays || options.defaults)
                object.players = [];
            if (options.defaults) {
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.roomId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.roomId = options.longs === String ? "0" : 0;
                object.rountIndex = 0;
                object.errorCode = options.enums === String ? "Success" : 1;
            }
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (typeof message.roomId === "number")
                    object.roomId = options.longs === String ? String(message.roomId) : message.roomId;
                else
                    object.roomId = options.longs === String ? $util.Long.prototype.toString.call(message.roomId) : options.longs === Number ? new $util.LongBits(message.roomId.low >>> 0, message.roomId.high >>> 0).toNumber() : message.roomId;
            if (message.rountIndex != null && message.hasOwnProperty("rountIndex"))
                object.rountIndex = message.rountIndex;
            if (message.players && message.players.length) {
                object.players = [];
                for (var j = 0; j < message.players.length; ++j)
                    object.players[j] = $root.pb.PlayerInfo.toObject(message.players[j], options);
            }
            if (message.errorCode != null && message.hasOwnProperty("errorCode"))
                object.errorCode = options.enums === String ? $root.pb.GS2CEnterRoomRet.ErrorCode[message.errorCode] : message.errorCode;
            return object;
        };

        /**
         * Converts this GS2CEnterRoomRet to JSON.
         * @function toJSON
         * @memberof pb.GS2CEnterRoomRet
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CEnterRoomRet.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        /**
         * ErrorCode enum.
         * @name pb.GS2CEnterRoomRet.ErrorCode
         * @enum {string}
         * @property {number} Success=1 Success value
         * @property {number} Fail=2 Fail value
         * @property {number} GameStart=3 GameStart value
         * @property {number} NeedPassword=4 NeedPassword value
         */
        GS2CEnterRoomRet.ErrorCode = (function() {
            var valuesById = {}, values = Object.create(valuesById);
            values[valuesById[1] = "Success"] = 1;
            values[valuesById[2] = "Fail"] = 2;
            values[valuesById[3] = "GameStart"] = 3;
            values[valuesById[4] = "NeedPassword"] = 4;
            return values;
        })();

        return GS2CEnterRoomRet;
    })();

    pb.GS2CTurnToBet = (function() {

        /**
         * Properties of a GS2CTurnToBet.
         * @memberof pb
         * @interface IGS2CTurnToBet
         * @property {number|Long|null} [roomId] GS2CTurnToBet roomId
         */

        /**
         * Constructs a new GS2CTurnToBet.
         * @memberof pb
         * @classdesc Represents a GS2CTurnToBet.
         * @implements IGS2CTurnToBet
         * @constructor
         * @param {pb.IGS2CTurnToBet=} [properties] Properties to set
         */
        function GS2CTurnToBet(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CTurnToBet roomId.
         * @member {number|Long} roomId
         * @memberof pb.GS2CTurnToBet
         * @instance
         */
        GS2CTurnToBet.prototype.roomId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * Creates a new GS2CTurnToBet instance using the specified properties.
         * @function create
         * @memberof pb.GS2CTurnToBet
         * @static
         * @param {pb.IGS2CTurnToBet=} [properties] Properties to set
         * @returns {pb.GS2CTurnToBet} GS2CTurnToBet instance
         */
        GS2CTurnToBet.create = function create(properties) {
            return new GS2CTurnToBet(properties);
        };

        /**
         * Encodes the specified GS2CTurnToBet message. Does not implicitly {@link pb.GS2CTurnToBet.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CTurnToBet
         * @static
         * @param {pb.IGS2CTurnToBet} message GS2CTurnToBet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CTurnToBet.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                writer.uint32(/* id 1, wireType 0 =*/8).int64(message.roomId);
            return writer;
        };

        /**
         * Encodes the specified GS2CTurnToBet message, length delimited. Does not implicitly {@link pb.GS2CTurnToBet.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CTurnToBet
         * @static
         * @param {pb.IGS2CTurnToBet} message GS2CTurnToBet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CTurnToBet.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CTurnToBet message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CTurnToBet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CTurnToBet} GS2CTurnToBet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CTurnToBet.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CTurnToBet();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.roomId = reader.int64();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            return message;
        };

        /**
         * Decodes a GS2CTurnToBet message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CTurnToBet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CTurnToBet} GS2CTurnToBet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CTurnToBet.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CTurnToBet message.
         * @function verify
         * @memberof pb.GS2CTurnToBet
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CTurnToBet.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (!$util.isInteger(message.roomId) && !(message.roomId && $util.isInteger(message.roomId.low) && $util.isInteger(message.roomId.high)))
                    return "roomId: integer|Long expected";
            return null;
        };

        /**
         * Creates a GS2CTurnToBet message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CTurnToBet
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CTurnToBet} GS2CTurnToBet
         */
        GS2CTurnToBet.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CTurnToBet)
                return object;
            var message = new $root.pb.GS2CTurnToBet();
            if (object.roomId != null)
                if ($util.Long)
                    (message.roomId = $util.Long.fromValue(object.roomId)).unsigned = false;
                else if (typeof object.roomId === "string")
                    message.roomId = parseInt(object.roomId, 10);
                else if (typeof object.roomId === "number")
                    message.roomId = object.roomId;
                else if (typeof object.roomId === "object")
                    message.roomId = new $util.LongBits(object.roomId.low >>> 0, object.roomId.high >>> 0).toNumber();
            return message;
        };

        /**
         * Creates a plain object from a GS2CTurnToBet message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CTurnToBet
         * @static
         * @param {pb.GS2CTurnToBet} message GS2CTurnToBet
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CTurnToBet.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults)
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.roomId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.roomId = options.longs === String ? "0" : 0;
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (typeof message.roomId === "number")
                    object.roomId = options.longs === String ? String(message.roomId) : message.roomId;
                else
                    object.roomId = options.longs === String ? $util.Long.prototype.toString.call(message.roomId) : options.longs === Number ? new $util.LongBits(message.roomId.low >>> 0, message.roomId.high >>> 0).toNumber() : message.roomId;
            return object;
        };

        /**
         * Converts this GS2CTurnToBet to JSON.
         * @function toJSON
         * @memberof pb.GS2CTurnToBet
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CTurnToBet.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return GS2CTurnToBet;
    })();

    pb.C2GSBet = (function() {

        /**
         * Properties of a C2GSBet.
         * @memberof pb
         * @interface IC2GSBet
         * @property {number|Long} roomId C2GSBet roomId
         * @property {number} rountIndex C2GSBet rountIndex
         * @property {pb.Side} betSide C2GSBet betSide
         * @property {number|Long} bet C2GSBet bet
         */

        /**
         * Constructs a new C2GSBet.
         * @memberof pb
         * @classdesc Represents a C2GSBet.
         * @implements IC2GSBet
         * @constructor
         * @param {pb.IC2GSBet=} [properties] Properties to set
         */
        function C2GSBet(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * C2GSBet roomId.
         * @member {number|Long} roomId
         * @memberof pb.C2GSBet
         * @instance
         */
        C2GSBet.prototype.roomId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * C2GSBet rountIndex.
         * @member {number} rountIndex
         * @memberof pb.C2GSBet
         * @instance
         */
        C2GSBet.prototype.rountIndex = 0;

        /**
         * C2GSBet betSide.
         * @member {pb.Side} betSide
         * @memberof pb.C2GSBet
         * @instance
         */
        C2GSBet.prototype.betSide = 0;

        /**
         * C2GSBet bet.
         * @member {number|Long} bet
         * @memberof pb.C2GSBet
         * @instance
         */
        C2GSBet.prototype.bet = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * Creates a new C2GSBet instance using the specified properties.
         * @function create
         * @memberof pb.C2GSBet
         * @static
         * @param {pb.IC2GSBet=} [properties] Properties to set
         * @returns {pb.C2GSBet} C2GSBet instance
         */
        C2GSBet.create = function create(properties) {
            return new C2GSBet(properties);
        };

        /**
         * Encodes the specified C2GSBet message. Does not implicitly {@link pb.C2GSBet.verify|verify} messages.
         * @function encode
         * @memberof pb.C2GSBet
         * @static
         * @param {pb.IC2GSBet} message C2GSBet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSBet.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int64(message.roomId);
            writer.uint32(/* id 2, wireType 0 =*/16).int32(message.rountIndex);
            writer.uint32(/* id 3, wireType 0 =*/24).int32(message.betSide);
            writer.uint32(/* id 4, wireType 0 =*/32).int64(message.bet);
            return writer;
        };

        /**
         * Encodes the specified C2GSBet message, length delimited. Does not implicitly {@link pb.C2GSBet.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.C2GSBet
         * @static
         * @param {pb.IC2GSBet} message C2GSBet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSBet.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a C2GSBet message from the specified reader or buffer.
         * @function decode
         * @memberof pb.C2GSBet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.C2GSBet} C2GSBet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSBet.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.C2GSBet();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.roomId = reader.int64();
                    break;
                case 2:
                    message.rountIndex = reader.int32();
                    break;
                case 3:
                    message.betSide = reader.int32();
                    break;
                case 4:
                    message.bet = reader.int64();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("roomId"))
                throw $util.ProtocolError("missing required 'roomId'", { instance: message });
            if (!message.hasOwnProperty("rountIndex"))
                throw $util.ProtocolError("missing required 'rountIndex'", { instance: message });
            if (!message.hasOwnProperty("betSide"))
                throw $util.ProtocolError("missing required 'betSide'", { instance: message });
            if (!message.hasOwnProperty("bet"))
                throw $util.ProtocolError("missing required 'bet'", { instance: message });
            return message;
        };

        /**
         * Decodes a C2GSBet message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.C2GSBet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.C2GSBet} C2GSBet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSBet.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a C2GSBet message.
         * @function verify
         * @memberof pb.C2GSBet
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        C2GSBet.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isInteger(message.roomId) && !(message.roomId && $util.isInteger(message.roomId.low) && $util.isInteger(message.roomId.high)))
                return "roomId: integer|Long expected";
            if (!$util.isInteger(message.rountIndex))
                return "rountIndex: integer expected";
            switch (message.betSide) {
            default:
                return "betSide: enum value expected";
            case 0:
            case 1:
                break;
            }
            if (!$util.isInteger(message.bet) && !(message.bet && $util.isInteger(message.bet.low) && $util.isInteger(message.bet.high)))
                return "bet: integer|Long expected";
            return null;
        };

        /**
         * Creates a C2GSBet message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.C2GSBet
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.C2GSBet} C2GSBet
         */
        C2GSBet.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.C2GSBet)
                return object;
            var message = new $root.pb.C2GSBet();
            if (object.roomId != null)
                if ($util.Long)
                    (message.roomId = $util.Long.fromValue(object.roomId)).unsigned = false;
                else if (typeof object.roomId === "string")
                    message.roomId = parseInt(object.roomId, 10);
                else if (typeof object.roomId === "number")
                    message.roomId = object.roomId;
                else if (typeof object.roomId === "object")
                    message.roomId = new $util.LongBits(object.roomId.low >>> 0, object.roomId.high >>> 0).toNumber();
            if (object.rountIndex != null)
                message.rountIndex = object.rountIndex | 0;
            switch (object.betSide) {
            case "BLUE":
            case 0:
                message.betSide = 0;
                break;
            case "RED":
            case 1:
                message.betSide = 1;
                break;
            }
            if (object.bet != null)
                if ($util.Long)
                    (message.bet = $util.Long.fromValue(object.bet)).unsigned = false;
                else if (typeof object.bet === "string")
                    message.bet = parseInt(object.bet, 10);
                else if (typeof object.bet === "number")
                    message.bet = object.bet;
                else if (typeof object.bet === "object")
                    message.bet = new $util.LongBits(object.bet.low >>> 0, object.bet.high >>> 0).toNumber();
            return message;
        };

        /**
         * Creates a plain object from a C2GSBet message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.C2GSBet
         * @static
         * @param {pb.C2GSBet} message C2GSBet
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        C2GSBet.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.roomId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.roomId = options.longs === String ? "0" : 0;
                object.rountIndex = 0;
                object.betSide = options.enums === String ? "BLUE" : 0;
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.bet = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.bet = options.longs === String ? "0" : 0;
            }
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (typeof message.roomId === "number")
                    object.roomId = options.longs === String ? String(message.roomId) : message.roomId;
                else
                    object.roomId = options.longs === String ? $util.Long.prototype.toString.call(message.roomId) : options.longs === Number ? new $util.LongBits(message.roomId.low >>> 0, message.roomId.high >>> 0).toNumber() : message.roomId;
            if (message.rountIndex != null && message.hasOwnProperty("rountIndex"))
                object.rountIndex = message.rountIndex;
            if (message.betSide != null && message.hasOwnProperty("betSide"))
                object.betSide = options.enums === String ? $root.pb.Side[message.betSide] : message.betSide;
            if (message.bet != null && message.hasOwnProperty("bet"))
                if (typeof message.bet === "number")
                    object.bet = options.longs === String ? String(message.bet) : message.bet;
                else
                    object.bet = options.longs === String ? $util.Long.prototype.toString.call(message.bet) : options.longs === Number ? new $util.LongBits(message.bet.low >>> 0, message.bet.high >>> 0).toNumber() : message.bet;
            return object;
        };

        /**
         * Converts this C2GSBet to JSON.
         * @function toJSON
         * @memberof pb.C2GSBet
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        C2GSBet.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return C2GSBet;
    })();

    pb.GS2CBetRet = (function() {

        /**
         * Properties of a GS2CBetRet.
         * @memberof pb
         * @interface IGS2CBetRet
         * @property {pb.GS2CBetRet.ErrorCode} errorCode GS2CBetRet errorCode
         */

        /**
         * Constructs a new GS2CBetRet.
         * @memberof pb
         * @classdesc Represents a GS2CBetRet.
         * @implements IGS2CBetRet
         * @constructor
         * @param {pb.IGS2CBetRet=} [properties] Properties to set
         */
        function GS2CBetRet(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CBetRet errorCode.
         * @member {pb.GS2CBetRet.ErrorCode} errorCode
         * @memberof pb.GS2CBetRet
         * @instance
         */
        GS2CBetRet.prototype.errorCode = 1;

        /**
         * Creates a new GS2CBetRet instance using the specified properties.
         * @function create
         * @memberof pb.GS2CBetRet
         * @static
         * @param {pb.IGS2CBetRet=} [properties] Properties to set
         * @returns {pb.GS2CBetRet} GS2CBetRet instance
         */
        GS2CBetRet.create = function create(properties) {
            return new GS2CBetRet(properties);
        };

        /**
         * Encodes the specified GS2CBetRet message. Does not implicitly {@link pb.GS2CBetRet.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CBetRet
         * @static
         * @param {pb.IGS2CBetRet} message GS2CBetRet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CBetRet.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 2, wireType 0 =*/16).int32(message.errorCode);
            return writer;
        };

        /**
         * Encodes the specified GS2CBetRet message, length delimited. Does not implicitly {@link pb.GS2CBetRet.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CBetRet
         * @static
         * @param {pb.IGS2CBetRet} message GS2CBetRet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CBetRet.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CBetRet message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CBetRet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CBetRet} GS2CBetRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CBetRet.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CBetRet();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 2:
                    message.errorCode = reader.int32();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("errorCode"))
                throw $util.ProtocolError("missing required 'errorCode'", { instance: message });
            return message;
        };

        /**
         * Decodes a GS2CBetRet message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CBetRet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CBetRet} GS2CBetRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CBetRet.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CBetRet message.
         * @function verify
         * @memberof pb.GS2CBetRet
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CBetRet.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            switch (message.errorCode) {
            default:
                return "errorCode: enum value expected";
            case 1:
            case 2:
            case 3:
            case 4:
                break;
            }
            return null;
        };

        /**
         * Creates a GS2CBetRet message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CBetRet
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CBetRet} GS2CBetRet
         */
        GS2CBetRet.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CBetRet)
                return object;
            var message = new $root.pb.GS2CBetRet();
            switch (object.errorCode) {
            case "Success":
            case 1:
                message.errorCode = 1;
                break;
            case "IndexError":
            case 2:
                message.errorCode = 2;
                break;
            case "CoinLess":
            case 3:
                message.errorCode = 3;
                break;
            case "Fail":
            case 4:
                message.errorCode = 4;
                break;
            }
            return message;
        };

        /**
         * Creates a plain object from a GS2CBetRet message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CBetRet
         * @static
         * @param {pb.GS2CBetRet} message GS2CBetRet
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CBetRet.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults)
                object.errorCode = options.enums === String ? "Success" : 1;
            if (message.errorCode != null && message.hasOwnProperty("errorCode"))
                object.errorCode = options.enums === String ? $root.pb.GS2CBetRet.ErrorCode[message.errorCode] : message.errorCode;
            return object;
        };

        /**
         * Converts this GS2CBetRet to JSON.
         * @function toJSON
         * @memberof pb.GS2CBetRet
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CBetRet.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        /**
         * ErrorCode enum.
         * @name pb.GS2CBetRet.ErrorCode
         * @enum {string}
         * @property {number} Success=1 Success value
         * @property {number} IndexError=2 IndexError value
         * @property {number} CoinLess=3 CoinLess value
         * @property {number} Fail=4 Fail value
         */
        GS2CBetRet.ErrorCode = (function() {
            var valuesById = {}, values = Object.create(valuesById);
            values[valuesById[1] = "Success"] = 1;
            values[valuesById[2] = "IndexError"] = 2;
            values[valuesById[3] = "CoinLess"] = 3;
            values[valuesById[4] = "Fail"] = 4;
            return values;
        })();

        return GS2CBetRet;
    })();

    pb.GS2CBetInfo = (function() {

        /**
         * Properties of a GS2CBetInfo.
         * @memberof pb
         * @interface IGS2CBetInfo
         * @property {number|Long} roomId GS2CBetInfo roomId
         * @property {number} rountIndex GS2CBetInfo rountIndex
         * @property {Array.<pb.IBetInfo>|null} [infoList] GS2CBetInfo infoList
         */

        /**
         * Constructs a new GS2CBetInfo.
         * @memberof pb
         * @classdesc Represents a GS2CBetInfo.
         * @implements IGS2CBetInfo
         * @constructor
         * @param {pb.IGS2CBetInfo=} [properties] Properties to set
         */
        function GS2CBetInfo(properties) {
            this.infoList = [];
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CBetInfo roomId.
         * @member {number|Long} roomId
         * @memberof pb.GS2CBetInfo
         * @instance
         */
        GS2CBetInfo.prototype.roomId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * GS2CBetInfo rountIndex.
         * @member {number} rountIndex
         * @memberof pb.GS2CBetInfo
         * @instance
         */
        GS2CBetInfo.prototype.rountIndex = 0;

        /**
         * GS2CBetInfo infoList.
         * @member {Array.<pb.IBetInfo>} infoList
         * @memberof pb.GS2CBetInfo
         * @instance
         */
        GS2CBetInfo.prototype.infoList = $util.emptyArray;

        /**
         * Creates a new GS2CBetInfo instance using the specified properties.
         * @function create
         * @memberof pb.GS2CBetInfo
         * @static
         * @param {pb.IGS2CBetInfo=} [properties] Properties to set
         * @returns {pb.GS2CBetInfo} GS2CBetInfo instance
         */
        GS2CBetInfo.create = function create(properties) {
            return new GS2CBetInfo(properties);
        };

        /**
         * Encodes the specified GS2CBetInfo message. Does not implicitly {@link pb.GS2CBetInfo.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CBetInfo
         * @static
         * @param {pb.IGS2CBetInfo} message GS2CBetInfo message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CBetInfo.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int64(message.roomId);
            writer.uint32(/* id 2, wireType 0 =*/16).int32(message.rountIndex);
            if (message.infoList != null && message.infoList.length)
                for (var i = 0; i < message.infoList.length; ++i)
                    $root.pb.BetInfo.encode(message.infoList[i], writer.uint32(/* id 3, wireType 2 =*/26).fork()).ldelim();
            return writer;
        };

        /**
         * Encodes the specified GS2CBetInfo message, length delimited. Does not implicitly {@link pb.GS2CBetInfo.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CBetInfo
         * @static
         * @param {pb.IGS2CBetInfo} message GS2CBetInfo message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CBetInfo.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CBetInfo message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CBetInfo
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CBetInfo} GS2CBetInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CBetInfo.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CBetInfo();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.roomId = reader.int64();
                    break;
                case 2:
                    message.rountIndex = reader.int32();
                    break;
                case 3:
                    if (!(message.infoList && message.infoList.length))
                        message.infoList = [];
                    message.infoList.push($root.pb.BetInfo.decode(reader, reader.uint32()));
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("roomId"))
                throw $util.ProtocolError("missing required 'roomId'", { instance: message });
            if (!message.hasOwnProperty("rountIndex"))
                throw $util.ProtocolError("missing required 'rountIndex'", { instance: message });
            return message;
        };

        /**
         * Decodes a GS2CBetInfo message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CBetInfo
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CBetInfo} GS2CBetInfo
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CBetInfo.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CBetInfo message.
         * @function verify
         * @memberof pb.GS2CBetInfo
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CBetInfo.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isInteger(message.roomId) && !(message.roomId && $util.isInteger(message.roomId.low) && $util.isInteger(message.roomId.high)))
                return "roomId: integer|Long expected";
            if (!$util.isInteger(message.rountIndex))
                return "rountIndex: integer expected";
            if (message.infoList != null && message.hasOwnProperty("infoList")) {
                if (!Array.isArray(message.infoList))
                    return "infoList: array expected";
                for (var i = 0; i < message.infoList.length; ++i) {
                    var error = $root.pb.BetInfo.verify(message.infoList[i]);
                    if (error)
                        return "infoList." + error;
                }
            }
            return null;
        };

        /**
         * Creates a GS2CBetInfo message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CBetInfo
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CBetInfo} GS2CBetInfo
         */
        GS2CBetInfo.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CBetInfo)
                return object;
            var message = new $root.pb.GS2CBetInfo();
            if (object.roomId != null)
                if ($util.Long)
                    (message.roomId = $util.Long.fromValue(object.roomId)).unsigned = false;
                else if (typeof object.roomId === "string")
                    message.roomId = parseInt(object.roomId, 10);
                else if (typeof object.roomId === "number")
                    message.roomId = object.roomId;
                else if (typeof object.roomId === "object")
                    message.roomId = new $util.LongBits(object.roomId.low >>> 0, object.roomId.high >>> 0).toNumber();
            if (object.rountIndex != null)
                message.rountIndex = object.rountIndex | 0;
            if (object.infoList) {
                if (!Array.isArray(object.infoList))
                    throw TypeError(".pb.GS2CBetInfo.infoList: array expected");
                message.infoList = [];
                for (var i = 0; i < object.infoList.length; ++i) {
                    if (typeof object.infoList[i] !== "object")
                        throw TypeError(".pb.GS2CBetInfo.infoList: object expected");
                    message.infoList[i] = $root.pb.BetInfo.fromObject(object.infoList[i]);
                }
            }
            return message;
        };

        /**
         * Creates a plain object from a GS2CBetInfo message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CBetInfo
         * @static
         * @param {pb.GS2CBetInfo} message GS2CBetInfo
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CBetInfo.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.arrays || options.defaults)
                object.infoList = [];
            if (options.defaults) {
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.roomId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.roomId = options.longs === String ? "0" : 0;
                object.rountIndex = 0;
            }
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (typeof message.roomId === "number")
                    object.roomId = options.longs === String ? String(message.roomId) : message.roomId;
                else
                    object.roomId = options.longs === String ? $util.Long.prototype.toString.call(message.roomId) : options.longs === Number ? new $util.LongBits(message.roomId.low >>> 0, message.roomId.high >>> 0).toNumber() : message.roomId;
            if (message.rountIndex != null && message.hasOwnProperty("rountIndex"))
                object.rountIndex = message.rountIndex;
            if (message.infoList && message.infoList.length) {
                object.infoList = [];
                for (var j = 0; j < message.infoList.length; ++j)
                    object.infoList[j] = $root.pb.BetInfo.toObject(message.infoList[j], options);
            }
            return object;
        };

        /**
         * Converts this GS2CBetInfo to JSON.
         * @function toJSON
         * @memberof pb.GS2CBetInfo
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CBetInfo.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return GS2CBetInfo;
    })();

    pb.GS2CGameResults = (function() {

        /**
         * Properties of a GS2CGameResults.
         * @memberof pb
         * @interface IGS2CGameResults
         * @property {boolean} results GS2CGameResults results
         * @property {pb.IPlayerInfo} info GS2CGameResults info
         */

        /**
         * Constructs a new GS2CGameResults.
         * @memberof pb
         * @classdesc Represents a GS2CGameResults.
         * @implements IGS2CGameResults
         * @constructor
         * @param {pb.IGS2CGameResults=} [properties] Properties to set
         */
        function GS2CGameResults(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CGameResults results.
         * @member {boolean} results
         * @memberof pb.GS2CGameResults
         * @instance
         */
        GS2CGameResults.prototype.results = false;

        /**
         * GS2CGameResults info.
         * @member {pb.IPlayerInfo} info
         * @memberof pb.GS2CGameResults
         * @instance
         */
        GS2CGameResults.prototype.info = null;

        /**
         * Creates a new GS2CGameResults instance using the specified properties.
         * @function create
         * @memberof pb.GS2CGameResults
         * @static
         * @param {pb.IGS2CGameResults=} [properties] Properties to set
         * @returns {pb.GS2CGameResults} GS2CGameResults instance
         */
        GS2CGameResults.create = function create(properties) {
            return new GS2CGameResults(properties);
        };

        /**
         * Encodes the specified GS2CGameResults message. Does not implicitly {@link pb.GS2CGameResults.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CGameResults
         * @static
         * @param {pb.IGS2CGameResults} message GS2CGameResults message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CGameResults.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).bool(message.results);
            $root.pb.PlayerInfo.encode(message.info, writer.uint32(/* id 2, wireType 2 =*/18).fork()).ldelim();
            return writer;
        };

        /**
         * Encodes the specified GS2CGameResults message, length delimited. Does not implicitly {@link pb.GS2CGameResults.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CGameResults
         * @static
         * @param {pb.IGS2CGameResults} message GS2CGameResults message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CGameResults.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CGameResults message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CGameResults
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CGameResults} GS2CGameResults
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CGameResults.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CGameResults();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.results = reader.bool();
                    break;
                case 2:
                    message.info = $root.pb.PlayerInfo.decode(reader, reader.uint32());
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("results"))
                throw $util.ProtocolError("missing required 'results'", { instance: message });
            if (!message.hasOwnProperty("info"))
                throw $util.ProtocolError("missing required 'info'", { instance: message });
            return message;
        };

        /**
         * Decodes a GS2CGameResults message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CGameResults
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CGameResults} GS2CGameResults
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CGameResults.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CGameResults message.
         * @function verify
         * @memberof pb.GS2CGameResults
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CGameResults.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (typeof message.results !== "boolean")
                return "results: boolean expected";
            {
                var error = $root.pb.PlayerInfo.verify(message.info);
                if (error)
                    return "info." + error;
            }
            return null;
        };

        /**
         * Creates a GS2CGameResults message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CGameResults
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CGameResults} GS2CGameResults
         */
        GS2CGameResults.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CGameResults)
                return object;
            var message = new $root.pb.GS2CGameResults();
            if (object.results != null)
                message.results = Boolean(object.results);
            if (object.info != null) {
                if (typeof object.info !== "object")
                    throw TypeError(".pb.GS2CGameResults.info: object expected");
                message.info = $root.pb.PlayerInfo.fromObject(object.info);
            }
            return message;
        };

        /**
         * Creates a plain object from a GS2CGameResults message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CGameResults
         * @static
         * @param {pb.GS2CGameResults} message GS2CGameResults
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CGameResults.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults) {
                object.results = false;
                object.info = null;
            }
            if (message.results != null && message.hasOwnProperty("results"))
                object.results = message.results;
            if (message.info != null && message.hasOwnProperty("info"))
                object.info = $root.pb.PlayerInfo.toObject(message.info, options);
            return object;
        };

        /**
         * Converts this GS2CGameResults to JSON.
         * @function toJSON
         * @memberof pb.GS2CGameResults
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CGameResults.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return GS2CGameResults;
    })();

    pb.GS2CNewRoundStart = (function() {

        /**
         * Properties of a GS2CNewRoundStart.
         * @memberof pb
         * @interface IGS2CNewRoundStart
         * @property {number} rountIndex GS2CNewRoundStart rountIndex
         */

        /**
         * Constructs a new GS2CNewRoundStart.
         * @memberof pb
         * @classdesc Represents a GS2CNewRoundStart.
         * @implements IGS2CNewRoundStart
         * @constructor
         * @param {pb.IGS2CNewRoundStart=} [properties] Properties to set
         */
        function GS2CNewRoundStart(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CNewRoundStart rountIndex.
         * @member {number} rountIndex
         * @memberof pb.GS2CNewRoundStart
         * @instance
         */
        GS2CNewRoundStart.prototype.rountIndex = 0;

        /**
         * Creates a new GS2CNewRoundStart instance using the specified properties.
         * @function create
         * @memberof pb.GS2CNewRoundStart
         * @static
         * @param {pb.IGS2CNewRoundStart=} [properties] Properties to set
         * @returns {pb.GS2CNewRoundStart} GS2CNewRoundStart instance
         */
        GS2CNewRoundStart.create = function create(properties) {
            return new GS2CNewRoundStart(properties);
        };

        /**
         * Encodes the specified GS2CNewRoundStart message. Does not implicitly {@link pb.GS2CNewRoundStart.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CNewRoundStart
         * @static
         * @param {pb.IGS2CNewRoundStart} message GS2CNewRoundStart message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CNewRoundStart.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int32(message.rountIndex);
            return writer;
        };

        /**
         * Encodes the specified GS2CNewRoundStart message, length delimited. Does not implicitly {@link pb.GS2CNewRoundStart.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CNewRoundStart
         * @static
         * @param {pb.IGS2CNewRoundStart} message GS2CNewRoundStart message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CNewRoundStart.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CNewRoundStart message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CNewRoundStart
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CNewRoundStart} GS2CNewRoundStart
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CNewRoundStart.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CNewRoundStart();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.rountIndex = reader.int32();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("rountIndex"))
                throw $util.ProtocolError("missing required 'rountIndex'", { instance: message });
            return message;
        };

        /**
         * Decodes a GS2CNewRoundStart message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CNewRoundStart
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CNewRoundStart} GS2CNewRoundStart
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CNewRoundStart.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CNewRoundStart message.
         * @function verify
         * @memberof pb.GS2CNewRoundStart
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CNewRoundStart.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isInteger(message.rountIndex))
                return "rountIndex: integer expected";
            return null;
        };

        /**
         * Creates a GS2CNewRoundStart message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CNewRoundStart
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CNewRoundStart} GS2CNewRoundStart
         */
        GS2CNewRoundStart.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CNewRoundStart)
                return object;
            var message = new $root.pb.GS2CNewRoundStart();
            if (object.rountIndex != null)
                message.rountIndex = object.rountIndex | 0;
            return message;
        };

        /**
         * Creates a plain object from a GS2CNewRoundStart message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CNewRoundStart
         * @static
         * @param {pb.GS2CNewRoundStart} message GS2CNewRoundStart
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CNewRoundStart.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults)
                object.rountIndex = 0;
            if (message.rountIndex != null && message.hasOwnProperty("rountIndex"))
                object.rountIndex = message.rountIndex;
            return object;
        };

        /**
         * Converts this GS2CNewRoundStart to JSON.
         * @function toJSON
         * @memberof pb.GS2CNewRoundStart
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CNewRoundStart.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return GS2CNewRoundStart;
    })();

    pb.GS2CGameOver = (function() {

        /**
         * Properties of a GS2CGameOver.
         * @memberof pb
         * @interface IGS2CGameOver
         * @property {number|Long|null} [roomId] GS2CGameOver roomId
         */

        /**
         * Constructs a new GS2CGameOver.
         * @memberof pb
         * @classdesc Represents a GS2CGameOver.
         * @implements IGS2CGameOver
         * @constructor
         * @param {pb.IGS2CGameOver=} [properties] Properties to set
         */
        function GS2CGameOver(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CGameOver roomId.
         * @member {number|Long} roomId
         * @memberof pb.GS2CGameOver
         * @instance
         */
        GS2CGameOver.prototype.roomId = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * Creates a new GS2CGameOver instance using the specified properties.
         * @function create
         * @memberof pb.GS2CGameOver
         * @static
         * @param {pb.IGS2CGameOver=} [properties] Properties to set
         * @returns {pb.GS2CGameOver} GS2CGameOver instance
         */
        GS2CGameOver.create = function create(properties) {
            return new GS2CGameOver(properties);
        };

        /**
         * Encodes the specified GS2CGameOver message. Does not implicitly {@link pb.GS2CGameOver.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CGameOver
         * @static
         * @param {pb.IGS2CGameOver} message GS2CGameOver message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CGameOver.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                writer.uint32(/* id 1, wireType 0 =*/8).int64(message.roomId);
            return writer;
        };

        /**
         * Encodes the specified GS2CGameOver message, length delimited. Does not implicitly {@link pb.GS2CGameOver.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CGameOver
         * @static
         * @param {pb.IGS2CGameOver} message GS2CGameOver message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CGameOver.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CGameOver message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CGameOver
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CGameOver} GS2CGameOver
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CGameOver.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CGameOver();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.roomId = reader.int64();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            return message;
        };

        /**
         * Decodes a GS2CGameOver message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CGameOver
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CGameOver} GS2CGameOver
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CGameOver.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CGameOver message.
         * @function verify
         * @memberof pb.GS2CGameOver
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CGameOver.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (!$util.isInteger(message.roomId) && !(message.roomId && $util.isInteger(message.roomId.low) && $util.isInteger(message.roomId.high)))
                    return "roomId: integer|Long expected";
            return null;
        };

        /**
         * Creates a GS2CGameOver message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CGameOver
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CGameOver} GS2CGameOver
         */
        GS2CGameOver.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CGameOver)
                return object;
            var message = new $root.pb.GS2CGameOver();
            if (object.roomId != null)
                if ($util.Long)
                    (message.roomId = $util.Long.fromValue(object.roomId)).unsigned = false;
                else if (typeof object.roomId === "string")
                    message.roomId = parseInt(object.roomId, 10);
                else if (typeof object.roomId === "number")
                    message.roomId = object.roomId;
                else if (typeof object.roomId === "object")
                    message.roomId = new $util.LongBits(object.roomId.low >>> 0, object.roomId.high >>> 0).toNumber();
            return message;
        };

        /**
         * Creates a plain object from a GS2CGameOver message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CGameOver
         * @static
         * @param {pb.GS2CGameOver} message GS2CGameOver
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CGameOver.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults)
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.roomId = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.roomId = options.longs === String ? "0" : 0;
            if (message.roomId != null && message.hasOwnProperty("roomId"))
                if (typeof message.roomId === "number")
                    object.roomId = options.longs === String ? String(message.roomId) : message.roomId;
                else
                    object.roomId = options.longs === String ? $util.Long.prototype.toString.call(message.roomId) : options.longs === Number ? new $util.LongBits(message.roomId.low >>> 0, message.roomId.high >>> 0).toNumber() : message.roomId;
            return object;
        };

        /**
         * Converts this GS2CGameOver to JSON.
         * @function toJSON
         * @memberof pb.GS2CGameOver
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CGameOver.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return GS2CGameOver;
    })();

    pb.C2GSGMAddCoin = (function() {

        /**
         * Properties of a C2GSGMAddCoin.
         * @memberof pb
         * @interface IC2GSGMAddCoin
         * @property {number|Long} value C2GSGMAddCoin value
         */

        /**
         * Constructs a new C2GSGMAddCoin.
         * @memberof pb
         * @classdesc Represents a C2GSGMAddCoin.
         * @implements IC2GSGMAddCoin
         * @constructor
         * @param {pb.IC2GSGMAddCoin=} [properties] Properties to set
         */
        function C2GSGMAddCoin(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * C2GSGMAddCoin value.
         * @member {number|Long} value
         * @memberof pb.C2GSGMAddCoin
         * @instance
         */
        C2GSGMAddCoin.prototype.value = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

        /**
         * Creates a new C2GSGMAddCoin instance using the specified properties.
         * @function create
         * @memberof pb.C2GSGMAddCoin
         * @static
         * @param {pb.IC2GSGMAddCoin=} [properties] Properties to set
         * @returns {pb.C2GSGMAddCoin} C2GSGMAddCoin instance
         */
        C2GSGMAddCoin.create = function create(properties) {
            return new C2GSGMAddCoin(properties);
        };

        /**
         * Encodes the specified C2GSGMAddCoin message. Does not implicitly {@link pb.C2GSGMAddCoin.verify|verify} messages.
         * @function encode
         * @memberof pb.C2GSGMAddCoin
         * @static
         * @param {pb.IC2GSGMAddCoin} message C2GSGMAddCoin message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSGMAddCoin.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            writer.uint32(/* id 1, wireType 0 =*/8).int64(message.value);
            return writer;
        };

        /**
         * Encodes the specified C2GSGMAddCoin message, length delimited. Does not implicitly {@link pb.C2GSGMAddCoin.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.C2GSGMAddCoin
         * @static
         * @param {pb.IC2GSGMAddCoin} message C2GSGMAddCoin message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        C2GSGMAddCoin.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a C2GSGMAddCoin message from the specified reader or buffer.
         * @function decode
         * @memberof pb.C2GSGMAddCoin
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.C2GSGMAddCoin} C2GSGMAddCoin
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSGMAddCoin.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.C2GSGMAddCoin();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.value = reader.int64();
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("value"))
                throw $util.ProtocolError("missing required 'value'", { instance: message });
            return message;
        };

        /**
         * Decodes a C2GSGMAddCoin message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.C2GSGMAddCoin
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.C2GSGMAddCoin} C2GSGMAddCoin
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        C2GSGMAddCoin.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a C2GSGMAddCoin message.
         * @function verify
         * @memberof pb.C2GSGMAddCoin
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        C2GSGMAddCoin.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            if (!$util.isInteger(message.value) && !(message.value && $util.isInteger(message.value.low) && $util.isInteger(message.value.high)))
                return "value: integer|Long expected";
            return null;
        };

        /**
         * Creates a C2GSGMAddCoin message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.C2GSGMAddCoin
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.C2GSGMAddCoin} C2GSGMAddCoin
         */
        C2GSGMAddCoin.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.C2GSGMAddCoin)
                return object;
            var message = new $root.pb.C2GSGMAddCoin();
            if (object.value != null)
                if ($util.Long)
                    (message.value = $util.Long.fromValue(object.value)).unsigned = false;
                else if (typeof object.value === "string")
                    message.value = parseInt(object.value, 10);
                else if (typeof object.value === "number")
                    message.value = object.value;
                else if (typeof object.value === "object")
                    message.value = new $util.LongBits(object.value.low >>> 0, object.value.high >>> 0).toNumber();
            return message;
        };

        /**
         * Creates a plain object from a C2GSGMAddCoin message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.C2GSGMAddCoin
         * @static
         * @param {pb.C2GSGMAddCoin} message C2GSGMAddCoin
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        C2GSGMAddCoin.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults)
                if ($util.Long) {
                    var long = new $util.Long(0, 0, false);
                    object.value = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
                } else
                    object.value = options.longs === String ? "0" : 0;
            if (message.value != null && message.hasOwnProperty("value"))
                if (typeof message.value === "number")
                    object.value = options.longs === String ? String(message.value) : message.value;
                else
                    object.value = options.longs === String ? $util.Long.prototype.toString.call(message.value) : options.longs === Number ? new $util.LongBits(message.value.low >>> 0, message.value.high >>> 0).toNumber() : message.value;
            return object;
        };

        /**
         * Converts this C2GSGMAddCoin to JSON.
         * @function toJSON
         * @memberof pb.C2GSGMAddCoin
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        C2GSGMAddCoin.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return C2GSGMAddCoin;
    })();

    pb.GS2CGMAddCoinRet = (function() {

        /**
         * Properties of a GS2CGMAddCoinRet.
         * @memberof pb
         * @interface IGS2CGMAddCoinRet
         * @property {pb.IPlayerInfo} user GS2CGMAddCoinRet user
         */

        /**
         * Constructs a new GS2CGMAddCoinRet.
         * @memberof pb
         * @classdesc Represents a GS2CGMAddCoinRet.
         * @implements IGS2CGMAddCoinRet
         * @constructor
         * @param {pb.IGS2CGMAddCoinRet=} [properties] Properties to set
         */
        function GS2CGMAddCoinRet(properties) {
            if (properties)
                for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                    if (properties[keys[i]] != null)
                        this[keys[i]] = properties[keys[i]];
        }

        /**
         * GS2CGMAddCoinRet user.
         * @member {pb.IPlayerInfo} user
         * @memberof pb.GS2CGMAddCoinRet
         * @instance
         */
        GS2CGMAddCoinRet.prototype.user = null;

        /**
         * Creates a new GS2CGMAddCoinRet instance using the specified properties.
         * @function create
         * @memberof pb.GS2CGMAddCoinRet
         * @static
         * @param {pb.IGS2CGMAddCoinRet=} [properties] Properties to set
         * @returns {pb.GS2CGMAddCoinRet} GS2CGMAddCoinRet instance
         */
        GS2CGMAddCoinRet.create = function create(properties) {
            return new GS2CGMAddCoinRet(properties);
        };

        /**
         * Encodes the specified GS2CGMAddCoinRet message. Does not implicitly {@link pb.GS2CGMAddCoinRet.verify|verify} messages.
         * @function encode
         * @memberof pb.GS2CGMAddCoinRet
         * @static
         * @param {pb.IGS2CGMAddCoinRet} message GS2CGMAddCoinRet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CGMAddCoinRet.encode = function encode(message, writer) {
            if (!writer)
                writer = $Writer.create();
            $root.pb.PlayerInfo.encode(message.user, writer.uint32(/* id 1, wireType 2 =*/10).fork()).ldelim();
            return writer;
        };

        /**
         * Encodes the specified GS2CGMAddCoinRet message, length delimited. Does not implicitly {@link pb.GS2CGMAddCoinRet.verify|verify} messages.
         * @function encodeDelimited
         * @memberof pb.GS2CGMAddCoinRet
         * @static
         * @param {pb.IGS2CGMAddCoinRet} message GS2CGMAddCoinRet message or plain object to encode
         * @param {$protobuf.Writer} [writer] Writer to encode to
         * @returns {$protobuf.Writer} Writer
         */
        GS2CGMAddCoinRet.encodeDelimited = function encodeDelimited(message, writer) {
            return this.encode(message, writer).ldelim();
        };

        /**
         * Decodes a GS2CGMAddCoinRet message from the specified reader or buffer.
         * @function decode
         * @memberof pb.GS2CGMAddCoinRet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @param {number} [length] Message length if known beforehand
         * @returns {pb.GS2CGMAddCoinRet} GS2CGMAddCoinRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CGMAddCoinRet.decode = function decode(reader, length) {
            if (!(reader instanceof $Reader))
                reader = $Reader.create(reader);
            var end = length === undefined ? reader.len : reader.pos + length, message = new $root.pb.GS2CGMAddCoinRet();
            while (reader.pos < end) {
                var tag = reader.uint32();
                switch (tag >>> 3) {
                case 1:
                    message.user = $root.pb.PlayerInfo.decode(reader, reader.uint32());
                    break;
                default:
                    reader.skipType(tag & 7);
                    break;
                }
            }
            if (!message.hasOwnProperty("user"))
                throw $util.ProtocolError("missing required 'user'", { instance: message });
            return message;
        };

        /**
         * Decodes a GS2CGMAddCoinRet message from the specified reader or buffer, length delimited.
         * @function decodeDelimited
         * @memberof pb.GS2CGMAddCoinRet
         * @static
         * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
         * @returns {pb.GS2CGMAddCoinRet} GS2CGMAddCoinRet
         * @throws {Error} If the payload is not a reader or valid buffer
         * @throws {$protobuf.util.ProtocolError} If required fields are missing
         */
        GS2CGMAddCoinRet.decodeDelimited = function decodeDelimited(reader) {
            if (!(reader instanceof $Reader))
                reader = new $Reader(reader);
            return this.decode(reader, reader.uint32());
        };

        /**
         * Verifies a GS2CGMAddCoinRet message.
         * @function verify
         * @memberof pb.GS2CGMAddCoinRet
         * @static
         * @param {Object.<string,*>} message Plain object to verify
         * @returns {string|null} `null` if valid, otherwise the reason why it is not
         */
        GS2CGMAddCoinRet.verify = function verify(message) {
            if (typeof message !== "object" || message === null)
                return "object expected";
            {
                var error = $root.pb.PlayerInfo.verify(message.user);
                if (error)
                    return "user." + error;
            }
            return null;
        };

        /**
         * Creates a GS2CGMAddCoinRet message from a plain object. Also converts values to their respective internal types.
         * @function fromObject
         * @memberof pb.GS2CGMAddCoinRet
         * @static
         * @param {Object.<string,*>} object Plain object
         * @returns {pb.GS2CGMAddCoinRet} GS2CGMAddCoinRet
         */
        GS2CGMAddCoinRet.fromObject = function fromObject(object) {
            if (object instanceof $root.pb.GS2CGMAddCoinRet)
                return object;
            var message = new $root.pb.GS2CGMAddCoinRet();
            if (object.user != null) {
                if (typeof object.user !== "object")
                    throw TypeError(".pb.GS2CGMAddCoinRet.user: object expected");
                message.user = $root.pb.PlayerInfo.fromObject(object.user);
            }
            return message;
        };

        /**
         * Creates a plain object from a GS2CGMAddCoinRet message. Also converts values to other types if specified.
         * @function toObject
         * @memberof pb.GS2CGMAddCoinRet
         * @static
         * @param {pb.GS2CGMAddCoinRet} message GS2CGMAddCoinRet
         * @param {$protobuf.IConversionOptions} [options] Conversion options
         * @returns {Object.<string,*>} Plain object
         */
        GS2CGMAddCoinRet.toObject = function toObject(message, options) {
            if (!options)
                options = {};
            var object = {};
            if (options.defaults)
                object.user = null;
            if (message.user != null && message.hasOwnProperty("user"))
                object.user = $root.pb.PlayerInfo.toObject(message.user, options);
            return object;
        };

        /**
         * Converts this GS2CGMAddCoinRet to JSON.
         * @function toJSON
         * @memberof pb.GS2CGMAddCoinRet
         * @instance
         * @returns {Object.<string,*>} JSON object
         */
        GS2CGMAddCoinRet.prototype.toJSON = function toJSON() {
            return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
        };

        return GS2CGMAddCoinRet;
    })();

    return pb;
})();

module.exports = $root;
