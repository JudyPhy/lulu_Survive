window.__require = function e(t, n, r) {
function o(s, a) {
if (!n[s]) {
if (!t[s]) {
var u = s.split("/");
u = u[u.length - 1];
if (!t[u]) {
var c = "function" == typeof __require && __require;
if (!a && c) return c(u, !0);
if (i) return i(u, !0);
throw new Error("Cannot find module '" + s + "'");
}
}
var l = n[s] = {
exports: {}
};
t[s][0].call(l.exports, function(e) {
return o(t[s][1][e] || e);
}, l, l.exports, e, t, n, r);
}
return n[s].exports;
}
for (var i = "function" == typeof __require && __require, s = 0; s < r.length; s++) o(r[s]);
return o;
}({
1: [ function(e, t, n) {
"use strict";
t.exports = function(e, t) {
var n = new Array(arguments.length - 1), r = 0, o = 2, i = !0;
for (;o < arguments.length; ) n[r++] = arguments[o++];
return new Promise(function(o, s) {
n[r] = function(e) {
if (i) {
i = !1;
if (e) s(e); else {
for (var t = new Array(arguments.length - 1), n = 0; n < t.length; ) t[n++] = arguments[n];
o.apply(null, t);
}
}
};
try {
e.apply(t || null, n);
} catch (e) {
if (i) {
i = !1;
s(e);
}
}
});
};
}, {} ],
2: [ function(e, t, n) {
"use strict";
var r = n;
r.length = function(e) {
var t = e.length;
if (!t) return 0;
for (var n = 0; --t % 4 > 1 && "=" === e.charAt(t); ) ++n;
return Math.ceil(3 * e.length) / 4 - n;
};
for (var o = new Array(64), i = new Array(123), s = 0; s < 64; ) i[o[s] = s < 26 ? s + 65 : s < 52 ? s + 71 : s < 62 ? s - 4 : s - 59 | 43] = s++;
r.encode = function(e, t, n) {
for (var r, i = null, s = [], a = 0, u = 0; t < n; ) {
var c = e[t++];
switch (u) {
case 0:
s[a++] = o[c >> 2];
r = (3 & c) << 4;
u = 1;
break;

case 1:
s[a++] = o[r | c >> 4];
r = (15 & c) << 2;
u = 2;
break;

case 2:
s[a++] = o[r | c >> 6];
s[a++] = o[63 & c];
u = 0;
}
if (a > 8191) {
(i || (i = [])).push(String.fromCharCode.apply(String, s));
a = 0;
}
}
if (u) {
s[a++] = o[r];
s[a++] = 61;
1 === u && (s[a++] = 61);
}
if (i) {
a && i.push(String.fromCharCode.apply(String, s.slice(0, a)));
return i.join("");
}
return String.fromCharCode.apply(String, s.slice(0, a));
};
r.decode = function(e, t, n) {
for (var r, o = n, s = 0, a = 0; a < e.length; ) {
var u = e.charCodeAt(a++);
if (61 === u && s > 1) break;
if (void 0 === (u = i[u])) throw Error("invalid encoding");
switch (s) {
case 0:
r = u;
s = 1;
break;

case 1:
t[n++] = r << 2 | (48 & u) >> 4;
r = u;
s = 2;
break;

case 2:
t[n++] = (15 & r) << 4 | (60 & u) >> 2;
r = u;
s = 3;
break;

case 3:
t[n++] = (3 & r) << 6 | u;
s = 0;
}
}
if (1 === s) throw Error("invalid encoding");
return n - o;
};
r.test = function(e) {
return /^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$/.test(e);
};
}, {} ],
3: [ function(e, t, n) {
"use strict";
t.exports = r;
function r() {
this._listeners = {};
}
r.prototype.on = function(e, t, n) {
(this._listeners[e] || (this._listeners[e] = [])).push({
fn: t,
ctx: n || this
});
return this;
};
r.prototype.off = function(e, t) {
if (void 0 === e) this._listeners = {}; else if (void 0 === t) this._listeners[e] = []; else for (var n = this._listeners[e], r = 0; r < n.length; ) n[r].fn === t ? n.splice(r, 1) : ++r;
return this;
};
r.prototype.emit = function(e) {
var t = this._listeners[e];
if (t) {
for (var n = [], r = 1; r < arguments.length; ) n.push(arguments[r++]);
for (r = 0; r < t.length; ) t[r].fn.apply(t[r++].ctx, n);
}
return this;
};
}, {} ],
4: [ function(e, t, n) {
"use strict";
t.exports = r(r);
function r(e) {
"undefined" != typeof Float32Array ? function() {
var t = new Float32Array([ -0 ]), n = new Uint8Array(t.buffer), r = 128 === n[3];
function o(e, r, o) {
t[0] = e;
r[o] = n[0];
r[o + 1] = n[1];
r[o + 2] = n[2];
r[o + 3] = n[3];
}
function i(e, r, o) {
t[0] = e;
r[o] = n[3];
r[o + 1] = n[2];
r[o + 2] = n[1];
r[o + 3] = n[0];
}
e.writeFloatLE = r ? o : i;
e.writeFloatBE = r ? i : o;
function s(e, r) {
n[0] = e[r];
n[1] = e[r + 1];
n[2] = e[r + 2];
n[3] = e[r + 3];
return t[0];
}
function a(e, r) {
n[3] = e[r];
n[2] = e[r + 1];
n[1] = e[r + 2];
n[0] = e[r + 3];
return t[0];
}
e.readFloatLE = r ? s : a;
e.readFloatBE = r ? a : s;
}() : function() {
function t(e, t, n, r) {
var o = t < 0 ? 1 : 0;
o && (t = -t);
if (0 === t) e(1 / t > 0 ? 0 : 2147483648, n, r); else if (isNaN(t)) e(2143289344, n, r); else if (t > 3.4028234663852886e38) e((o << 31 | 2139095040) >>> 0, n, r); else if (t < 1.1754943508222875e-38) e((o << 31 | Math.round(t / 1.401298464324817e-45)) >>> 0, n, r); else {
var i = Math.floor(Math.log(t) / Math.LN2);
e((o << 31 | i + 127 << 23 | 8388607 & Math.round(t * Math.pow(2, -i) * 8388608)) >>> 0, n, r);
}
}
e.writeFloatLE = t.bind(null, o);
e.writeFloatBE = t.bind(null, i);
function n(e, t, n) {
var r = e(t, n), o = 2 * (r >> 31) + 1, i = r >>> 23 & 255, s = 8388607 & r;
return 255 === i ? s ? NaN : Infinity * o : 0 === i ? 1.401298464324817e-45 * o * s : o * Math.pow(2, i - 150) * (s + 8388608);
}
e.readFloatLE = n.bind(null, s);
e.readFloatBE = n.bind(null, a);
}();
"undefined" != typeof Float64Array ? function() {
var t = new Float64Array([ -0 ]), n = new Uint8Array(t.buffer), r = 128 === n[7];
function o(e, r, o) {
t[0] = e;
r[o] = n[0];
r[o + 1] = n[1];
r[o + 2] = n[2];
r[o + 3] = n[3];
r[o + 4] = n[4];
r[o + 5] = n[5];
r[o + 6] = n[6];
r[o + 7] = n[7];
}
function i(e, r, o) {
t[0] = e;
r[o] = n[7];
r[o + 1] = n[6];
r[o + 2] = n[5];
r[o + 3] = n[4];
r[o + 4] = n[3];
r[o + 5] = n[2];
r[o + 6] = n[1];
r[o + 7] = n[0];
}
e.writeDoubleLE = r ? o : i;
e.writeDoubleBE = r ? i : o;
function s(e, r) {
n[0] = e[r];
n[1] = e[r + 1];
n[2] = e[r + 2];
n[3] = e[r + 3];
n[4] = e[r + 4];
n[5] = e[r + 5];
n[6] = e[r + 6];
n[7] = e[r + 7];
return t[0];
}
function a(e, r) {
n[7] = e[r];
n[6] = e[r + 1];
n[5] = e[r + 2];
n[4] = e[r + 3];
n[3] = e[r + 4];
n[2] = e[r + 5];
n[1] = e[r + 6];
n[0] = e[r + 7];
return t[0];
}
e.readDoubleLE = r ? s : a;
e.readDoubleBE = r ? a : s;
}() : function() {
function t(e, t, n, r, o, i) {
var s = r < 0 ? 1 : 0;
s && (r = -r);
if (0 === r) {
e(0, o, i + t);
e(1 / r > 0 ? 0 : 2147483648, o, i + n);
} else if (isNaN(r)) {
e(0, o, i + t);
e(2146959360, o, i + n);
} else if (r > 1.7976931348623157e308) {
e(0, o, i + t);
e((s << 31 | 2146435072) >>> 0, o, i + n);
} else {
var a;
if (r < 2.2250738585072014e-308) {
e((a = r / 5e-324) >>> 0, o, i + t);
e((s << 31 | a / 4294967296) >>> 0, o, i + n);
} else {
var u = Math.floor(Math.log(r) / Math.LN2);
1024 === u && (u = 1023);
e(4503599627370496 * (a = r * Math.pow(2, -u)) >>> 0, o, i + t);
e((s << 31 | u + 1023 << 20 | 1048576 * a & 1048575) >>> 0, o, i + n);
}
}
}
e.writeDoubleLE = t.bind(null, o, 0, 4);
e.writeDoubleBE = t.bind(null, i, 4, 0);
function n(e, t, n, r, o) {
var i = e(r, o + t), s = e(r, o + n), a = 2 * (s >> 31) + 1, u = s >>> 20 & 2047, c = 4294967296 * (1048575 & s) + i;
return 2047 === u ? c ? NaN : Infinity * a : 0 === u ? 5e-324 * a * c : a * Math.pow(2, u - 1075) * (c + 4503599627370496);
}
e.readDoubleLE = n.bind(null, s, 0, 4);
e.readDoubleBE = n.bind(null, a, 4, 0);
}();
return e;
}
function o(e, t, n) {
t[n] = 255 & e;
t[n + 1] = e >>> 8 & 255;
t[n + 2] = e >>> 16 & 255;
t[n + 3] = e >>> 24;
}
function i(e, t, n) {
t[n] = e >>> 24;
t[n + 1] = e >>> 16 & 255;
t[n + 2] = e >>> 8 & 255;
t[n + 3] = 255 & e;
}
function s(e, t) {
return (e[t] | e[t + 1] << 8 | e[t + 2] << 16 | e[t + 3] << 24) >>> 0;
}
function a(e, t) {
return (e[t] << 24 | e[t + 1] << 16 | e[t + 2] << 8 | e[t + 3]) >>> 0;
}
}, {} ],
5: [ function(require, module, exports) {
"use strict";
module.exports = inquire;
function inquire(moduleName) {
try {
var mod = eval("quire".replace(/^/, "re"))(moduleName);
if (mod && (mod.length || Object.keys(mod).length)) return mod;
} catch (e) {}
return null;
}
}, {} ],
6: [ function(e, t, n) {
"use strict";
t.exports = function(e, t, n) {
var r = n || 8192, o = r >>> 1, i = null, s = r;
return function(n) {
if (n < 1 || n > o) return e(n);
if (s + n > r) {
i = e(r);
s = 0;
}
var a = t.call(i, s, s += n);
7 & s && (s = 1 + (7 | s));
return a;
};
};
}, {} ],
7: [ function(e, t, n) {
"use strict";
var r = n;
r.length = function(e) {
for (var t = 0, n = 0, r = 0; r < e.length; ++r) if ((n = e.charCodeAt(r)) < 128) t += 1; else if (n < 2048) t += 2; else if (55296 == (64512 & n) && 56320 == (64512 & e.charCodeAt(r + 1))) {
++r;
t += 4;
} else t += 3;
return t;
};
r.read = function(e, t, n) {
if (n - t < 1) return "";
for (var r, o = null, i = [], s = 0; t < n; ) {
if ((r = e[t++]) < 128) i[s++] = r; else if (r > 191 && r < 224) i[s++] = (31 & r) << 6 | 63 & e[t++]; else if (r > 239 && r < 365) {
r = ((7 & r) << 18 | (63 & e[t++]) << 12 | (63 & e[t++]) << 6 | 63 & e[t++]) - 65536;
i[s++] = 55296 + (r >> 10);
i[s++] = 56320 + (1023 & r);
} else i[s++] = (15 & r) << 12 | (63 & e[t++]) << 6 | 63 & e[t++];
if (s > 8191) {
(o || (o = [])).push(String.fromCharCode.apply(String, i));
s = 0;
}
}
if (o) {
s && o.push(String.fromCharCode.apply(String, i.slice(0, s)));
return o.join("");
}
return String.fromCharCode.apply(String, i.slice(0, s));
};
r.write = function(e, t, n) {
for (var r, o, i = n, s = 0; s < e.length; ++s) if ((r = e.charCodeAt(s)) < 128) t[n++] = r; else if (r < 2048) {
t[n++] = r >> 6 | 192;
t[n++] = 63 & r | 128;
} else if (55296 == (64512 & r) && 56320 == (64512 & (o = e.charCodeAt(s + 1)))) {
r = 65536 + ((1023 & r) << 10) + (1023 & o);
++s;
t[n++] = r >> 18 | 240;
t[n++] = r >> 12 & 63 | 128;
t[n++] = r >> 6 & 63 | 128;
t[n++] = 63 & r | 128;
} else {
t[n++] = r >> 12 | 224;
t[n++] = r >> 6 & 63 | 128;
t[n++] = 63 & r | 128;
}
return n - i;
};
}, {} ],
8: [ function(e, t, n) {
"use strict";
t.exports = e("./src/index-minimal");
}, {
"./src/index-minimal": 9
} ],
9: [ function(e, t, n) {
"use strict";
var r = n;
r.build = "minimal";
r.Writer = e("./writer");
r.BufferWriter = e("./writer_buffer");
r.Reader = e("./reader");
r.BufferReader = e("./reader_buffer");
r.util = e("./util/minimal");
r.rpc = e("./rpc");
r.roots = e("./roots");
r.configure = o;
function o() {
r.Reader._configure(r.BufferReader);
r.util._configure();
}
r.Writer._configure(r.BufferWriter);
o();
}, {
"./reader": 10,
"./reader_buffer": 11,
"./roots": 12,
"./rpc": 13,
"./util/minimal": 16,
"./writer": 17,
"./writer_buffer": 18
} ],
10: [ function(e, t, n) {
"use strict";
t.exports = u;
var r, o = e("./util/minimal"), i = o.LongBits, s = o.utf8;
function a(e, t) {
return RangeError("index out of range: " + e.pos + " + " + (t || 1) + " > " + e.len);
}
function u(e) {
this.buf = e;
this.pos = 0;
this.len = e.length;
}
var c = "undefined" != typeof Uint8Array ? function(e) {
if (e instanceof Uint8Array || Array.isArray(e)) return new u(e);
throw Error("illegal buffer");
} : function(e) {
if (Array.isArray(e)) return new u(e);
throw Error("illegal buffer");
};
u.create = o.Buffer ? function(e) {
return (u.create = function(e) {
return o.Buffer.isBuffer(e) ? new r(e) : c(e);
})(e);
} : c;
u.prototype._slice = o.Array.prototype.subarray || o.Array.prototype.slice;
u.prototype.uint32 = function() {
var e = 4294967295;
return function() {
e = (127 & this.buf[this.pos]) >>> 0;
if (this.buf[this.pos++] < 128) return e;
e = (e | (127 & this.buf[this.pos]) << 7) >>> 0;
if (this.buf[this.pos++] < 128) return e;
e = (e | (127 & this.buf[this.pos]) << 14) >>> 0;
if (this.buf[this.pos++] < 128) return e;
e = (e | (127 & this.buf[this.pos]) << 21) >>> 0;
if (this.buf[this.pos++] < 128) return e;
e = (e | (15 & this.buf[this.pos]) << 28) >>> 0;
if (this.buf[this.pos++] < 128) return e;
if ((this.pos += 5) > this.len) {
this.pos = this.len;
throw a(this, 10);
}
return e;
};
}();
u.prototype.int32 = function() {
return 0 | this.uint32();
};
u.prototype.sint32 = function() {
var e = this.uint32();
return e >>> 1 ^ -(1 & e) | 0;
};
function l() {
var e = new i(0, 0), t = 0;
if (!(this.len - this.pos > 4)) {
for (;t < 3; ++t) {
if (this.pos >= this.len) throw a(this);
e.lo = (e.lo | (127 & this.buf[this.pos]) << 7 * t) >>> 0;
if (this.buf[this.pos++] < 128) return e;
}
e.lo = (e.lo | (127 & this.buf[this.pos++]) << 7 * t) >>> 0;
return e;
}
for (;t < 4; ++t) {
e.lo = (e.lo | (127 & this.buf[this.pos]) << 7 * t) >>> 0;
if (this.buf[this.pos++] < 128) return e;
}
e.lo = (e.lo | (127 & this.buf[this.pos]) << 28) >>> 0;
e.hi = (e.hi | (127 & this.buf[this.pos]) >> 4) >>> 0;
if (this.buf[this.pos++] < 128) return e;
t = 0;
if (this.len - this.pos > 4) for (;t < 5; ++t) {
e.hi = (e.hi | (127 & this.buf[this.pos]) << 7 * t + 3) >>> 0;
if (this.buf[this.pos++] < 128) return e;
} else for (;t < 5; ++t) {
if (this.pos >= this.len) throw a(this);
e.hi = (e.hi | (127 & this.buf[this.pos]) << 7 * t + 3) >>> 0;
if (this.buf[this.pos++] < 128) return e;
}
throw Error("invalid varint encoding");
}
u.prototype.bool = function() {
return 0 !== this.uint32();
};
function d(e, t) {
return (e[t - 4] | e[t - 3] << 8 | e[t - 2] << 16 | e[t - 1] << 24) >>> 0;
}
u.prototype.fixed32 = function() {
if (this.pos + 4 > this.len) throw a(this, 4);
return d(this.buf, this.pos += 4);
};
u.prototype.sfixed32 = function() {
if (this.pos + 4 > this.len) throw a(this, 4);
return 0 | d(this.buf, this.pos += 4);
};
function p() {
if (this.pos + 8 > this.len) throw a(this, 8);
return new i(d(this.buf, this.pos += 4), d(this.buf, this.pos += 4));
}
u.prototype.float = function() {
if (this.pos + 4 > this.len) throw a(this, 4);
var e = o.float.readFloatLE(this.buf, this.pos);
this.pos += 4;
return e;
};
u.prototype.double = function() {
if (this.pos + 8 > this.len) throw a(this, 4);
var e = o.float.readDoubleLE(this.buf, this.pos);
this.pos += 8;
return e;
};
u.prototype.bytes = function() {
var e = this.uint32(), t = this.pos, n = this.pos + e;
if (n > this.len) throw a(this, e);
this.pos += e;
return Array.isArray(this.buf) ? this.buf.slice(t, n) : t === n ? new this.buf.constructor(0) : this._slice.call(this.buf, t, n);
};
u.prototype.string = function() {
var e = this.bytes();
return s.read(e, 0, e.length);
};
u.prototype.skip = function(e) {
if ("number" == typeof e) {
if (this.pos + e > this.len) throw a(this, e);
this.pos += e;
} else do {
if (this.pos >= this.len) throw a(this);
} while (128 & this.buf[this.pos++]);
return this;
};
u.prototype.skipType = function(e) {
switch (e) {
case 0:
this.skip();
break;

case 1:
this.skip(8);
break;

case 2:
this.skip(this.uint32());
break;

case 3:
for (;4 != (e = 7 & this.uint32()); ) this.skipType(e);
break;

case 5:
this.skip(4);
break;

default:
throw Error("invalid wire type " + e + " at offset " + this.pos);
}
return this;
};
u._configure = function(e) {
r = e;
var t = o.Long ? "toLong" : "toNumber";
o.merge(u.prototype, {
int64: function() {
return l.call(this)[t](!1);
},
uint64: function() {
return l.call(this)[t](!0);
},
sint64: function() {
return l.call(this).zzDecode()[t](!1);
},
fixed64: function() {
return p.call(this)[t](!0);
},
sfixed64: function() {
return p.call(this)[t](!1);
}
});
};
}, {
"./util/minimal": 16
} ],
11: [ function(e, t, n) {
"use strict";
t.exports = i;
var r = e("./reader");
(i.prototype = Object.create(r.prototype)).constructor = i;
var o = e("./util/minimal");
function i(e) {
r.call(this, e);
}
o.Buffer && (i.prototype._slice = o.Buffer.prototype.slice);
i.prototype.string = function() {
var e = this.uint32();
return this.buf.utf8Slice(this.pos, this.pos = Math.min(this.pos + e, this.len));
};
}, {
"./reader": 10,
"./util/minimal": 16
} ],
12: [ function(e, t, n) {
"use strict";
t.exports = {};
}, {} ],
13: [ function(e, t, n) {
"use strict";
n.Service = e("./rpc/service");
}, {
"./rpc/service": 14
} ],
14: [ function(e, t, n) {
"use strict";
t.exports = o;
var r = e("../util/minimal");
(o.prototype = Object.create(r.EventEmitter.prototype)).constructor = o;
function o(e, t, n) {
if ("function" != typeof e) throw TypeError("rpcImpl must be a function");
r.EventEmitter.call(this);
this.rpcImpl = e;
this.requestDelimited = Boolean(t);
this.responseDelimited = Boolean(n);
}
o.prototype.rpcCall = function e(t, n, o, i, s) {
if (!i) throw TypeError("request must be specified");
var a = this;
if (!s) return r.asPromise(e, a, t, n, o, i);
if (a.rpcImpl) try {
return a.rpcImpl(t, n[a.requestDelimited ? "encodeDelimited" : "encode"](i).finish(), function(e, n) {
if (e) {
a.emit("error", e, t);
return s(e);
}
if (null !== n) {
if (!(n instanceof o)) try {
n = o[a.responseDelimited ? "decodeDelimited" : "decode"](n);
} catch (e) {
a.emit("error", e, t);
return s(e);
}
a.emit("data", n, t);
return s(null, n);
}
a.end(!0);
});
} catch (e) {
a.emit("error", e, t);
setTimeout(function() {
s(e);
}, 0);
return;
} else setTimeout(function() {
s(Error("already ended"));
}, 0);
};
o.prototype.end = function(e) {
if (this.rpcImpl) {
e || this.rpcImpl(null, null, null);
this.rpcImpl = null;
this.emit("end").off();
}
return this;
};
}, {
"../util/minimal": 16
} ],
15: [ function(e, t, n) {
"use strict";
t.exports = o;
var r = e("../util/minimal");
function o(e, t) {
this.lo = e >>> 0;
this.hi = t >>> 0;
}
var i = o.zero = new o(0, 0);
i.toNumber = function() {
return 0;
};
i.zzEncode = i.zzDecode = function() {
return this;
};
i.length = function() {
return 1;
};
var s = o.zeroHash = "\0\0\0\0\0\0\0\0";
o.fromNumber = function(e) {
if (0 === e) return i;
var t = e < 0;
t && (e = -e);
var n = e >>> 0, r = (e - n) / 4294967296 >>> 0;
if (t) {
r = ~r >>> 0;
n = ~n >>> 0;
if (++n > 4294967295) {
n = 0;
++r > 4294967295 && (r = 0);
}
}
return new o(n, r);
};
o.from = function(e) {
if ("number" == typeof e) return o.fromNumber(e);
if (r.isString(e)) {
if (!r.Long) return o.fromNumber(parseInt(e, 10));
e = r.Long.fromString(e);
}
return e.low || e.high ? new o(e.low >>> 0, e.high >>> 0) : i;
};
o.prototype.toNumber = function(e) {
if (!e && this.hi >>> 31) {
var t = 1 + ~this.lo >>> 0, n = ~this.hi >>> 0;
t || (n = n + 1 >>> 0);
return -(t + 4294967296 * n);
}
return this.lo + 4294967296 * this.hi;
};
o.prototype.toLong = function(e) {
return r.Long ? new r.Long(0 | this.lo, 0 | this.hi, Boolean(e)) : {
low: 0 | this.lo,
high: 0 | this.hi,
unsigned: Boolean(e)
};
};
var a = String.prototype.charCodeAt;
o.fromHash = function(e) {
return e === s ? i : new o((a.call(e, 0) | a.call(e, 1) << 8 | a.call(e, 2) << 16 | a.call(e, 3) << 24) >>> 0, (a.call(e, 4) | a.call(e, 5) << 8 | a.call(e, 6) << 16 | a.call(e, 7) << 24) >>> 0);
};
o.prototype.toHash = function() {
return String.fromCharCode(255 & this.lo, this.lo >>> 8 & 255, this.lo >>> 16 & 255, this.lo >>> 24, 255 & this.hi, this.hi >>> 8 & 255, this.hi >>> 16 & 255, this.hi >>> 24);
};
o.prototype.zzEncode = function() {
var e = this.hi >> 31;
this.hi = ((this.hi << 1 | this.lo >>> 31) ^ e) >>> 0;
this.lo = (this.lo << 1 ^ e) >>> 0;
return this;
};
o.prototype.zzDecode = function() {
var e = -(1 & this.lo);
this.lo = ((this.lo >>> 1 | this.hi << 31) ^ e) >>> 0;
this.hi = (this.hi >>> 1 ^ e) >>> 0;
return this;
};
o.prototype.length = function() {
var e = this.lo, t = (this.lo >>> 28 | this.hi << 4) >>> 0, n = this.hi >>> 24;
return 0 === n ? 0 === t ? e < 16384 ? e < 128 ? 1 : 2 : e < 2097152 ? 3 : 4 : t < 16384 ? t < 128 ? 5 : 6 : t < 2097152 ? 7 : 8 : n < 128 ? 9 : 10;
};
}, {
"../util/minimal": 16
} ],
16: [ function(e, t, n) {
(function(t) {
"use strict";
var r = n;
r.asPromise = e("@protobufjs/aspromise");
r.base64 = e("@protobufjs/base64");
r.EventEmitter = e("@protobufjs/eventemitter");
r.float = e("@protobufjs/float");
r.inquire = e("@protobufjs/inquire");
r.utf8 = e("@protobufjs/utf8");
r.pool = e("@protobufjs/pool");
r.LongBits = e("./longbits");
r.global = "undefined" != typeof window && window || "undefined" != typeof t && t || "undefined" != typeof self && self || this;
r.emptyArray = Object.freeze ? Object.freeze([]) : [];
r.emptyObject = Object.freeze ? Object.freeze({}) : {};
r.isNode = Boolean(r.global.process && r.global.process.versions && r.global.process.versions.node);
r.isInteger = Number.isInteger || function(e) {
return "number" == typeof e && isFinite(e) && Math.floor(e) === e;
};
r.isString = function(e) {
return "string" == typeof e || e instanceof String;
};
r.isObject = function(e) {
return e && "object" == typeof e;
};
r.isset = r.isSet = function(e, t) {
var n = e[t];
return !(null == n || !e.hasOwnProperty(t)) && ("object" != typeof n || (Array.isArray(n) ? n.length : Object.keys(n).length) > 0);
};
r.Buffer = function() {
try {
var e = r.inquire("buffer").Buffer;
return e.prototype.utf8Write ? e : null;
} catch (e) {
return null;
}
}();
r._Buffer_from = null;
r._Buffer_allocUnsafe = null;
r.newBuffer = function(e) {
return "number" == typeof e ? r.Buffer ? r._Buffer_allocUnsafe(e) : new r.Array(e) : r.Buffer ? r._Buffer_from(e) : "undefined" == typeof Uint8Array ? e : new Uint8Array(e);
};
r.Array = "undefined" != typeof Uint8Array ? Uint8Array : Array;
r.Long = r.global.dcodeIO && r.global.dcodeIO.Long || r.global.Long || r.inquire("long");
r.key2Re = /^true|false|0|1$/;
r.key32Re = /^-?(?:0|[1-9][0-9]*)$/;
r.key64Re = /^(?:[\\x00-\\xff]{8}|-?(?:0|[1-9][0-9]*))$/;
r.longToHash = function(e) {
return e ? r.LongBits.from(e).toHash() : r.LongBits.zeroHash;
};
r.longFromHash = function(e, t) {
var n = r.LongBits.fromHash(e);
return r.Long ? r.Long.fromBits(n.lo, n.hi, t) : n.toNumber(Boolean(t));
};
function o(e, t, n) {
for (var r = Object.keys(t), o = 0; o < r.length; ++o) void 0 !== e[r[o]] && n || (e[r[o]] = t[r[o]]);
return e;
}
r.merge = o;
r.lcFirst = function(e) {
return e.charAt(0).toLowerCase() + e.substring(1);
};
function i(e) {
function t(e, n) {
if (!(this instanceof t)) return new t(e, n);
Object.defineProperty(this, "message", {
get: function() {
return e;
}
});
Error.captureStackTrace ? Error.captureStackTrace(this, t) : Object.defineProperty(this, "stack", {
value: new Error().stack || ""
});
n && o(this, n);
}
(t.prototype = Object.create(Error.prototype)).constructor = t;
Object.defineProperty(t.prototype, "name", {
get: function() {
return e;
}
});
t.prototype.toString = function() {
return this.name + ": " + this.message;
};
return t;
}
r.newError = i;
r.ProtocolError = i("ProtocolError");
r.oneOfGetter = function(e) {
for (var t = {}, n = 0; n < e.length; ++n) t[e[n]] = 1;
return function() {
for (var e = Object.keys(this), n = e.length - 1; n > -1; --n) if (1 === t[e[n]] && void 0 !== this[e[n]] && null !== this[e[n]]) return e[n];
};
};
r.oneOfSetter = function(e) {
return function(t) {
for (var n = 0; n < e.length; ++n) e[n] !== t && delete this[e[n]];
};
};
r.toJSONOptions = {
longs: String,
enums: String,
bytes: String,
json: !0
};
r._configure = function() {
var e = r.Buffer;
if (e) {
r._Buffer_from = e.from !== Uint8Array.from && e.from || function(t, n) {
return new e(t, n);
};
r._Buffer_allocUnsafe = e.allocUnsafe || function(t) {
return new e(t);
};
} else r._Buffer_from = r._Buffer_allocUnsafe = null;
};
}).call(this, "undefined" != typeof global ? global : "undefined" != typeof self ? self : "undefined" != typeof window ? window : {});
}, {
"./longbits": 15,
"@protobufjs/aspromise": 1,
"@protobufjs/base64": 2,
"@protobufjs/eventemitter": 3,
"@protobufjs/float": 4,
"@protobufjs/inquire": 5,
"@protobufjs/pool": 6,
"@protobufjs/utf8": 7
} ],
17: [ function(e, t, n) {
"use strict";
t.exports = l;
var r, o = e("./util/minimal"), i = o.LongBits, s = o.base64, a = o.utf8;
function u(e, t, n) {
this.fn = e;
this.len = t;
this.next = void 0;
this.val = n;
}
function c() {}
function l() {
this.len = 0;
this.head = new u(c, 0, 0);
this.tail = this.head;
this.states = null;
}
l.create = o.Buffer ? function() {
return (l.create = function() {
return new r();
})();
} : function() {
return new l();
};
l.alloc = function(e) {
return new o.Array(e);
};
o.Array !== Array && (l.alloc = o.pool(l.alloc, o.Array.prototype.subarray));
l.prototype._push = function(e, t, n) {
this.tail = this.tail.next = new u(e, t, n);
this.len += t;
return this;
};
function d(e, t, n) {
t[n] = 255 & e;
}
function p(e, t) {
this.len = e;
this.next = void 0;
this.val = t;
}
p.prototype = Object.create(u.prototype);
p.prototype.fn = function(e, t, n) {
for (;e > 127; ) {
t[n++] = 127 & e | 128;
e >>>= 7;
}
t[n] = e;
};
l.prototype.uint32 = function(e) {
this.len += (this.tail = this.tail.next = new p((e >>>= 0) < 128 ? 1 : e < 16384 ? 2 : e < 2097152 ? 3 : e < 268435456 ? 4 : 5, e)).len;
return this;
};
l.prototype.int32 = function(e) {
return e < 0 ? this._push(f, 10, i.fromNumber(e)) : this.uint32(e);
};
l.prototype.sint32 = function(e) {
return this.uint32((e << 1 ^ e >> 31) >>> 0);
};
function f(e, t, n) {
for (;e.hi; ) {
t[n++] = 127 & e.lo | 128;
e.lo = (e.lo >>> 7 | e.hi << 25) >>> 0;
e.hi >>>= 7;
}
for (;e.lo > 127; ) {
t[n++] = 127 & e.lo | 128;
e.lo = e.lo >>> 7;
}
t[n++] = e.lo;
}
l.prototype.uint64 = function(e) {
var t = i.from(e);
return this._push(f, t.length(), t);
};
l.prototype.int64 = l.prototype.uint64;
l.prototype.sint64 = function(e) {
var t = i.from(e).zzEncode();
return this._push(f, t.length(), t);
};
l.prototype.bool = function(e) {
return this._push(d, 1, e ? 1 : 0);
};
function h(e, t, n) {
t[n] = 255 & e;
t[n + 1] = e >>> 8 & 255;
t[n + 2] = e >>> 16 & 255;
t[n + 3] = e >>> 24;
}
l.prototype.fixed32 = function(e) {
return this._push(h, 4, e >>> 0);
};
l.prototype.sfixed32 = l.prototype.fixed32;
l.prototype.fixed64 = function(e) {
var t = i.from(e);
return this._push(h, 4, t.lo)._push(h, 4, t.hi);
};
l.prototype.sfixed64 = l.prototype.fixed64;
l.prototype.float = function(e) {
return this._push(o.float.writeFloatLE, 4, e);
};
l.prototype.double = function(e) {
return this._push(o.float.writeDoubleLE, 8, e);
};
var g = o.Array.prototype.set ? function(e, t, n) {
t.set(e, n);
} : function(e, t, n) {
for (var r = 0; r < e.length; ++r) t[n + r] = e[r];
};
l.prototype.bytes = function(e) {
var t = e.length >>> 0;
if (!t) return this._push(d, 1, 0);
if (o.isString(e)) {
var n = l.alloc(t = s.length(e));
s.decode(e, n, 0);
e = n;
}
return this.uint32(t)._push(g, t, e);
};
l.prototype.string = function(e) {
var t = a.length(e);
return t ? this.uint32(t)._push(a.write, t, e) : this._push(d, 1, 0);
};
l.prototype.fork = function() {
this.states = new function(e) {
this.head = e.head;
this.tail = e.tail;
this.len = e.len;
this.next = e.states;
}(this);
this.head = this.tail = new u(c, 0, 0);
this.len = 0;
return this;
};
l.prototype.reset = function() {
if (this.states) {
this.head = this.states.head;
this.tail = this.states.tail;
this.len = this.states.len;
this.states = this.states.next;
} else {
this.head = this.tail = new u(c, 0, 0);
this.len = 0;
}
return this;
};
l.prototype.ldelim = function() {
var e = this.head, t = this.tail, n = this.len;
this.reset().uint32(n);
if (n) {
this.tail.next = e.next;
this.tail = t;
this.len += n;
}
return this;
};
l.prototype.finish = function() {
for (var e = this.head.next, t = this.constructor.alloc(this.len), n = 0; e; ) {
e.fn(e.val, t, n);
n += e.len;
e = e.next;
}
return t;
};
l._configure = function(e) {
r = e;
};
}, {
"./util/minimal": 16
} ],
18: [ function(e, t, n) {
"use strict";
t.exports = s;
var r = e("./writer");
(s.prototype = Object.create(r.prototype)).constructor = s;
var o = e("./util/minimal"), i = o.Buffer;
function s() {
r.call(this);
}
s.alloc = function(e) {
return (s.alloc = o._Buffer_allocUnsafe)(e);
};
var a = i && i.prototype instanceof Uint8Array && "set" === i.prototype.set.name ? function(e, t, n) {
t.set(e, n);
} : function(e, t, n) {
if (e.copy) e.copy(t, n, 0, e.length); else for (var r = 0; r < e.length; ) t[n++] = e[r++];
};
s.prototype.bytes = function(e) {
o.isString(e) && (e = o._Buffer_from(e, "base64"));
var t = e.length >>> 0;
this.uint32(t);
t && this._push(a, t, e);
return this;
};
function u(e, t, n) {
e.length < 40 ? o.utf8.write(e, t, n) : t.utf8Write(e, n);
}
s.prototype.string = function(e) {
var t = i.byteLength(e);
this.uint32(t);
t && this._push(u, t, e);
return this;
};
}, {
"./util/minimal": 16,
"./writer": 17
} ],
EventDispatch: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "7b8bcFDILZMvY9pnP64vjBV", "EventDispatch");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = e("./Observer"), o = function() {
function e() {}
e.register = function(t, n, o) {
e.listeners[t] || (e.listeners[t] = []);
e.listeners[t].push(new r.Observer(n, o));
};
e.remove = function(t, n, r) {
var o = e.listeners[t];
if (o) {
for (var i = o.length, s = 0; s < i; s++) {
if (o[s].compar(r)) {
o.splice(s, 1);
break;
}
}
0 == o.length && delete e.listeners[t];
}
};
e.fire = function(t) {
for (var n = [], r = 1; r < arguments.length; r++) n[r - 1] = arguments[r];
var o = e.listeners[t];
if (o) for (var i = o.length, s = 0; s < i; s++) {
var a = o[s];
a.notify.apply(a, [ t ].concat(n));
}
};
e.listeners = {};
return e;
}();
n.EventDispatch = o;
cc._RF.pop();
}, {
"./Observer": "Observer"
} ],
EventType: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "db71bev3BpHZoFDYDXio3mr", "EventType");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = function() {
function e() {}
e.NET_CONNECTING = "NET.CONNECTING";
e.NET_CONNECT_SUCCESS = "NET.CONNECT_SUCCESS";
e.NET_CONNECT_ERROR = "NET.CONNECT_ERROR";
e.ROOM_UPDATE_ROUND_INDEX = "ROOM.UPDATE_ROUND_INDEX";
e.ROOM_TURN_TO_BET = "ROOM.TURN_TO_BET";
return e;
}();
n.EventType = r;
cc._RF.pop();
}, {} ],
NetManager: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "5b772aAEp9EWJJwMZwEBBOF", "NetManager");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = cc._decorator, o = r.ccclass, i = (r.property, e("./../net/Network")), s = e("./../uimanager/uimanager"), a = e("./../uimanager/windowDefine"), u = e("./../handler/EventDispatch"), c = e("./../handler/EventType"), l = e("./../net/messageID"), d = e("./../ui/gameMessageHandler"), p = function(e) {
__extends(t, e);
function t() {
var t = null !== e && e.apply(this, arguments) || this;
t.connectState = 0;
t.reconnectTime = 0;
t.reconnectTimeLimite = 5;
t.messageHandlerArray = new Array();
return t;
}
n = t;
t.prototype.onLoad = function() {
n.Instance = this;
this.gameNet = new i.Network();
this.gameNet.connectGS("ws://localhost:9000");
};
t.prototype.start = function() {
this.registerNetHandler();
u.EventDispatch.register(c.EventType.NET_CONNECT_SUCCESS, this.connectedCallback, null);
};
t.prototype.registerNetHandler = function() {
this.registerMessageHandler(l.MessageID.MSG_GS2C_LOGIN_RET, d.GameMessageHandler.getInstance().RecieveGS2CLoginRet);
this.registerMessageHandler(l.MessageID.MSG_C2GS_ENTERROOM_RET, d.GameMessageHandler.getInstance().RecieveGS2CEnterRoomRet);
this.registerMessageHandler(l.MessageID.MSG_GS2C_NEW_ROUND, d.GameMessageHandler.getInstance().RecieveGS2CNewRoundStart);
this.registerMessageHandler(l.MessageID.MSG_GS2C_TURN_TO_BET, d.GameMessageHandler.getInstance().RecieveGS2CTurnToBet);
};
t.prototype.registerMessageHandler = function(e, t) {
for (var n = !1, r = 0, o = this.messageHandlerArray; r < o.length; r++) {
var i = o[r];
if (i.pid == e) {
n = !0;
console.log("MsgHandler[" + i.pid + "] has registered.");
break;
}
}
if (!n) {
var s = new f();
s.pid = e;
s.function = t;
this.messageHandlerArray.push(s);
}
};
t.prototype.reconnect = function() {
if (!(this.reconnectTime >= this.reconnectTimeLimite)) {
this.reconnectTime++;
console.log("reconnectTime=" + this.reconnectTime);
this.gameNet.connectGS("ws://localhost:9000");
}
};
t.prototype.update = function(e) {
var t = this.gameNet.getConnectState();
if (t != this.connectState) {
console.log("net connect state changed:" + t);
this.connectState = t;
1 == this.connectState || (2 == this.connectState ? console.log("connect is closing") : 3 == this.connectState && console.log("connect failed"));
}
this.procRevMsgLogic();
};
t.prototype.connectedCallback = function() {
s.UIManager.Instance.showWindow(a.WindowId.Login);
};
t.prototype.SendToGS = function(e, t) {
console.log("SendToGS [id:" + e + "][msg:" + t + "]");
if (null == this.gameNet || !this.gameNet.IsConnected()) {
console.log("ws disconnect，can't send message[msgid:" + e + "]");
return !1;
}
this.gameNet.Send(e, t);
};
t.prototype.procRevMsgLogic = function() {
for (var e = 10; this.gameNet.RevMessageArray.length > 0 && e > 0; ) {
e--;
var t = this.gameNet.RevMessageArray.shift();
if (null != t) {
for (var n = !1, r = 0, o = this.messageHandlerArray; r < o.length; r++) {
var i = o[r];
if (i.pid == t.pid) {
n = !0;
i.function(t.buffer);
break;
}
}
n || console.log("消息id: ", t.pid, "没有注册处理函数句柄!");
}
}
};
var n;
return t = n = __decorate([ o ], t);
}(cc.Component);
n.NetManager = p;
var f = function() {
function e() {}
e.prototype.init = function(e, t) {
this.pid = e;
this.function = t;
};
return e;
}();
n.MessageHandler = f;
cc._RF.pop();
}, {
"./../handler/EventDispatch": "EventDispatch",
"./../handler/EventType": "EventType",
"./../net/Network": "Network",
"./../net/messageID": "messageID",
"./../ui/gameMessageHandler": "gameMessageHandler",
"./../uimanager/uimanager": "uimanager",
"./../uimanager/windowDefine": "windowDefine"
} ],
Network: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "f6a76/1rHROEKmp4jQz/BAv", "Network");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = e("./../handler/EventDispatch"), o = e("./../handler/EventType"), i = function() {
function e() {
this.ws = null;
this.headPackageSize = 4;
this.msgIncompleteBuffer = null;
this.RevMessageArray = new Array();
}
e.prototype.connectGS = function(e) {
console.log("connectGS url=" + e);
var t = this;
t.ws = new WebSocket(e);
t.ws.onopen = function() {
console.log("connect success");
r.EventDispatch.fire(o.EventType.NET_CONNECT_SUCCESS);
};
t.ws.onmessage = function(e) {
t.splitMessage(e);
r.EventDispatch.fire(o.EventType.NET_CONNECT_SUCCESS);
};
t.ws.onclose = function() {
console.log("close ws");
};
t.ws.onerror = function() {
console.log("ws error");
};
};
e.prototype.splitMessage = function(e) {
var t = e.data, n = new Uint8Array(t), r = null;
if (null == this.msgIncompleteBuffer) r = n; else {
var o = new Uint8Array(this.msgIncompleteBuffer.length + n.length);
o.set(this.msgIncompleteBuffer, 0);
o.set(n, this.msgIncompleteBuffer.length);
this.msgIncompleteBuffer = null;
r = o;
}
for (var i = 0; i != r.length; ) {
if (r.length - i <= this.headPackageSize) {
(u = new Uint8Array(r.length - i)).set(r.subarray(i), 0);
this.msgIncompleteBuffer = u;
break;
}
var s = r.subarray(2, 4), a = this.Uint8ArrayToInt(s) + 2;
if (a > r.length - i) {
var u;
(u = new Uint8Array(r.length - i)).set(r.subarray(i), 0);
this.msgIncompleteBuffer = u;
break;
}
var c = r.subarray(i, i + 2), l = this.Uint8ArrayToInt(c);
console.log("receive message id = " + l);
var d = new Uint8Array(a - this.headPackageSize);
d.set(r.subarray(i + this.headPackageSize, i + a), 0);
console.log("receive message data = " + d);
this.BuildMessage(d, l);
i += a;
}
};
e.prototype.BuildMessage = function(e, t) {
var n = new s();
n.pid = t;
n.buffer = e;
this.RevMessageArray.push(n);
};
e.prototype.Uint8ArrayToInt = function(e) {
for (var t = 0, n = 0; n < e.length; n++) t |= e[n] << 8 * (e.length - n - 1);
return t;
};
e.prototype.getConnectState = function() {
return null != this.ws ? this.ws.readyState : -1;
};
e.prototype.IsConnected = function() {
return 1 == this.ws.readyState;
};
e.prototype.IntToUint8Array = function(e, t) {
for (var n = [], r = e.toString(2), o = 0; o < r.length; o++) n.push(parseInt(r[o]));
if (t) for (var i = n.length; i < t; i++) n.unshift(0);
for (var s = [], a = n.join(""), u = 0; u < t; u += 8) s.push(parseInt(a.slice(u, u + 8), 2));
return s;
};
e.prototype.Send = function(e, t) {
var n = new Uint8Array(t.length + 4), r = this.IntToUint8Array(e, 16), o = new Uint8Array(r);
n.set(o, 0);
var i = this.IntToUint8Array(t.length + 2, 16), s = new Uint8Array(i);
n.set(s, 2);
n.set(t.subarray(0, t.length), 4);
console.log("send ws");
this.ws.send(n.buffer);
};
return e;
}();
n.Network = i;
var s = function() {
return function() {};
}();
cc._RF.pop();
}, {
"./../handler/EventDispatch": "EventDispatch",
"./../handler/EventType": "EventType"
} ],
Observer: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "78240X2N+tGMoyW23FArMdB", "Observer");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = function() {
function e(e, t) {
this.callback = null;
this.context = null;
this.callback = e;
this.context = t;
}
e.prototype.notify = function() {
for (var e, t = [], n = 0; n < arguments.length; n++) t[n] = arguments[n];
(e = this.callback).call.apply(e, [ this.context ].concat(t));
};
e.prototype.compar = function(e) {
return e == this.context;
};
return e;
}();
n.Observer = r;
cc._RF.pop();
}, {} ],
gameMessageHandler: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "d62cbweB0JBB7bL74fN1Jbd", "gameMessageHandler");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = e("./../net/NetManager"), o = e("./../net/messageID"), i = e("./../pbProc/pb"), s = e("./../player/playerManager"), a = e("../uimanager/uimanager"), u = e("../uimanager/windowDefine"), c = e("../handler/EventDispatch"), l = e("../handler/EventType"), d = e("../player/player"), p = function() {
function e() {}
e.getInstance = function() {
null == e.instance && (e.instance = new e());
return e.instance;
};
e.prototype.SendC2GSLogin = function(e, t) {
var n = i.pb.C2GSLogin.create({
user: e,
password: t
}), s = i.pb.C2GSLogin.encode(n).finish();
r.NetManager.Instance.SendToGS(o.MessageID.MSG_C2GS_LOGIN, s);
};
e.prototype.RecieveGS2CLoginRet = function(e) {
var t = i.pb.GS2CLoginRet.decode(e);
console.log("RecieveGS2CLoginRet:", t.user, t.errorCode);
if (t.errorCode == i.pb.GS2CLoginRet.ErrorCode.Success) {
s.PlayerManager.getInstance().initMy(t.user);
a.UIManager.Instance.showWindow(u.WindowId.Hall);
} else console.error("login failed");
};
e.prototype.SendC2GSEnterRoom = function(e, t) {
var n = i.pb.C2GSEnterRoom.create({
roomId: e,
password: t
}), s = i.pb.C2GSEnterRoom.encode(n).finish();
r.NetManager.Instance.SendToGS(o.MessageID.MSG_C2GS_ENTERROOM, s);
};
e.prototype.RecieveGS2CEnterRoomRet = function(e) {
var t = i.pb.GS2CEnterRoomRet.decode(e);
console.log("RecieveGS2CEnterRoomRet:", t.roomId, t.errorCode);
if (t.errorCode == i.pb.GS2CEnterRoomRet.ErrorCode.Success) {
s.PlayerManager.getInstance().mMy.updateRoomId(t.roomId);
s.PlayerManager.getInstance().mMy.updateRoomRoundIndex(t.rountIndex);
a.UIManager.Instance.showWindow(u.WindowId.Room);
} else t.errorCode == i.pb.GS2CEnterRoomRet.ErrorCode.GameStart ? console.error("game start") : t.errorCode == i.pb.GS2CEnterRoomRet.ErrorCode.NeedPassword || console.error("fail");
};
e.prototype.RecieveGS2CNewRoundStart = function(e) {
var t = i.pb.GS2CNewRoundStart.decode(e);
console.log("RecieveGS2CNewRoundStart:", t.rountIndex);
s.PlayerManager.getInstance().mMy.updateRoomRoundIndex(t.rountIndex);
c.EventDispatch.fire(l.EventType.ROOM_UPDATE_ROUND_INDEX);
};
e.prototype.RecieveGS2CTurnToBet = function(e) {
console.log("RecieveGS2CTurnToBet");
s.PlayerManager.getInstance().mMy.updateGameState(d.GameState.Bet);
c.EventDispatch.fire(l.EventType.ROOM_TURN_TO_BET);
};
e.instance = null;
return e;
}();
n.GameMessageHandler = p;
cc._RF.pop();
}, {
"../handler/EventDispatch": "EventDispatch",
"../handler/EventType": "EventType",
"../player/player": "player",
"../uimanager/uimanager": "uimanager",
"../uimanager/windowDefine": "windowDefine",
"./../net/NetManager": "NetManager",
"./../net/messageID": "messageID",
"./../pbProc/pb": "pb",
"./../player/playerManager": "playerManager"
} ],
hall: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "e4359LssWRIQaVE2uzoV4uB", "hall");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = e("./gameMessageHandler"), o = e("../uimanager/uimanager"), i = cc._decorator, s = i.ccclass, a = (i.property, 
function(e) {
__extends(t, e);
function t() {
return null !== e && e.apply(this, arguments) || this;
}
t.prototype.onLoad = function() {
this.mButtonFree = cc.find("button_free", this.node).getComponent(cc.Button);
this.mButtonFree.node.on("click", this.onClickFree, this);
this.mButtonFriend = cc.find("button_friend", this.node).getComponent(cc.Button);
this.mButtonFriend.node.on("click", this.onClickFriend, this);
this.mButtonJoin = cc.find("button_join", this.node).getComponent(cc.Button);
this.mButtonJoin.node.on("click", this.onClickJoin, this);
o.UIManager.Instance.showPlayerInfoNode(!0);
};
t.prototype.start = function() {};
t.prototype.onClickFree = function(e) {
console.log("onClickFree");
r.GameMessageHandler.getInstance().SendC2GSEnterRoom(0, 0);
};
t.prototype.onClickFriend = function(e) {
console.log("onClickFriend");
};
t.prototype.onClickJoin = function(e) {
console.log("onClickJoin");
};
return t = __decorate([ s ], t);
}(cc.Component));
n.Hall = a;
cc._RF.pop();
}, {
"../uimanager/uimanager": "uimanager",
"./gameMessageHandler": "gameMessageHandler"
} ],
login: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "c87edW9jg9CT45LRnumm4DW", "login");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = cc._decorator, o = r.ccclass, i = (r.property, e("./gameMessageHandler")), s = function(e) {
__extends(t, e);
function t() {
var t = null !== e && e.apply(this, arguments) || this;
t.userEdit = null;
t.passwordEdit = null;
t.buttonLogin = null;
return t;
}
t.prototype.onLoad = function() {
this.userEdit = cc.find("Container/User", this.node).getComponent(cc.EditBox);
this.passwordEdit = cc.find("Container/Password", this.node).getComponent(cc.EditBox);
this.buttonLogin = cc.find("Container/ButtonLogin", this.node).getComponent(cc.Button);
this.buttonLogin.node.on("click", this.onClickLogin, this);
};
t.prototype.start = function() {};
t.prototype.onClickLogin = function(e) {
console.log("onClickLogin");
var t = this.userEdit.string, n = this.passwordEdit.string;
if (null == t || 0 == t.length || null == n || 0 == n.length) {
t = "lulu";
n = "123456";
}
i.GameMessageHandler.getInstance().SendC2GSLogin(t, n);
};
t.prototype.update = function(e) {};
return t = __decorate([ o ], t);
}(cc.Component);
n.Login = s;
cc._RF.pop();
}, {
"./gameMessageHandler": "gameMessageHandler"
} ],
messageID: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "44244mzIcFF7ICZYyNPX1Z6", "messageID");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = function() {
function e() {}
e.MSG_C2GS_LOGIN = 0;
e.MSG_GS2C_LOGIN_RET = 1;
e.MSG_C2GS_ENTERROOM = 3;
e.MSG_C2GS_ENTERROOM_RET = 4;
e.MSG_GS2C_NEW_ROUND = 5;
e.MSG_GS2C_TURN_TO_BET = 8;
return e;
}();
n.MessageID = r;
cc._RF.pop();
}, {} ],
myTool: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "6c5c7+fi09JmZBR/4buv7se", "myTool");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = function() {
function e() {}
e.AddChild = function(e, t) {
cc.loader.loadRes(t, function(t, n) {
if (t) console.log("载入资源失败, 原因:" + t); else if (n instanceof cc.Prefab) {
var r = cc.instantiate(n);
e.addChild(r);
} else console.log("载入的不是预制资源!");
});
};
e.getObj = function(e) {
var t = null;
cc.loader.loadRes(e, function(e, n) {
e ? console.log("载入资源失败, 原因:" + e) : n instanceof cc.Prefab ? t = cc.instantiate(n) : console.log("载入的不是预制资源!");
});
return t;
};
return e;
}();
n.MyTool = r;
cc._RF.pop();
}, {} ],
pb: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "559eafzXUVFk4F1ubiHc98W", "pb");
var r = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
return typeof e;
} : function(e) {
return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e;
}, o = e("protobufjs/minimal"), i = o.Reader, s = o.Writer, a = o.util, u = o.roots.default || (o.roots.default = {});
u.pb = function() {
var e = {};
e.PayMode = function() {
var e = {}, t = Object.create(e);
t[e[0] = "SHARE"] = 0;
t[e[1] = "OWNER"] = 1;
return t;
}();
e.GameRound = function() {
var e = {}, t = Object.create(e);
t[e[0] = "ROUND4"] = 0;
t[e[1] = "ROUND8"] = 1;
return t;
}();
e.PlayerInfo = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.playerId = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.prototype.nickname = "";
e.prototype.headicon = "";
e.prototype.card = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.prototype.coin = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(8).int64(e.playerId);
t.uint32(18).string(e.nickname);
t.uint32(26).string(e.headicon);
t.uint32(32).int64(e.card);
t.uint32(40).int64(e.coin);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.PlayerInfo(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.playerId = e.int64();
break;

case 2:
r.nickname = e.string();
break;

case 3:
r.headicon = e.string();
break;

case 4:
r.card = e.int64();
break;

case 5:
r.coin = e.int64();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("playerId")) throw a.ProtocolError("missing required 'playerId'", {
instance: r
});
if (!r.hasOwnProperty("nickname")) throw a.ProtocolError("missing required 'nickname'", {
instance: r
});
if (!r.hasOwnProperty("headicon")) throw a.ProtocolError("missing required 'headicon'", {
instance: r
});
if (!r.hasOwnProperty("card")) throw a.ProtocolError("missing required 'card'", {
instance: r
});
if (!r.hasOwnProperty("coin")) throw a.ProtocolError("missing required 'coin'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
return "object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e ? "object expected" : a.isInteger(e.playerId) || e.playerId && a.isInteger(e.playerId.low) && a.isInteger(e.playerId.high) ? a.isString(e.nickname) ? a.isString(e.headicon) ? a.isInteger(e.card) || e.card && a.isInteger(e.card.low) && a.isInteger(e.card.high) ? a.isInteger(e.coin) || e.coin && a.isInteger(e.coin.low) && a.isInteger(e.coin.high) ? null : "coin: integer|Long expected" : "card: integer|Long expected" : "headicon: string expected" : "nickname: string expected" : "playerId: integer|Long expected";
};
e.fromObject = function(e) {
if (e instanceof u.pb.PlayerInfo) return e;
var t = new u.pb.PlayerInfo();
null != e.playerId && (a.Long ? (t.playerId = a.Long.fromValue(e.playerId)).unsigned = !1 : "string" == typeof e.playerId ? t.playerId = parseInt(e.playerId, 10) : "number" == typeof e.playerId ? t.playerId = e.playerId : "object" === r(e.playerId) && (t.playerId = new a.LongBits(e.playerId.low >>> 0, e.playerId.high >>> 0).toNumber()));
null != e.nickname && (t.nickname = String(e.nickname));
null != e.headicon && (t.headicon = String(e.headicon));
null != e.card && (a.Long ? (t.card = a.Long.fromValue(e.card)).unsigned = !1 : "string" == typeof e.card ? t.card = parseInt(e.card, 10) : "number" == typeof e.card ? t.card = e.card : "object" === r(e.card) && (t.card = new a.LongBits(e.card.low >>> 0, e.card.high >>> 0).toNumber()));
null != e.coin && (a.Long ? (t.coin = a.Long.fromValue(e.coin)).unsigned = !1 : "string" == typeof e.coin ? t.coin = parseInt(e.coin, 10) : "number" == typeof e.coin ? t.coin = e.coin : "object" === r(e.coin) && (t.coin = new a.LongBits(e.coin.low >>> 0, e.coin.high >>> 0).toNumber()));
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
if (a.Long) {
var r = new a.Long(0, 0, !1);
n.playerId = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.playerId = t.longs === String ? "0" : 0;
n.nickname = "";
n.headicon = "";
if (a.Long) {
r = new a.Long(0, 0, !1);
n.card = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.card = t.longs === String ? "0" : 0;
if (a.Long) {
r = new a.Long(0, 0, !1);
n.coin = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.coin = t.longs === String ? "0" : 0;
}
null != e.playerId && e.hasOwnProperty("playerId") && ("number" == typeof e.playerId ? n.playerId = t.longs === String ? String(e.playerId) : e.playerId : n.playerId = t.longs === String ? a.Long.prototype.toString.call(e.playerId) : t.longs === Number ? new a.LongBits(e.playerId.low >>> 0, e.playerId.high >>> 0).toNumber() : e.playerId);
null != e.nickname && e.hasOwnProperty("nickname") && (n.nickname = e.nickname);
null != e.headicon && e.hasOwnProperty("headicon") && (n.headicon = e.headicon);
null != e.card && e.hasOwnProperty("card") && ("number" == typeof e.card ? n.card = t.longs === String ? String(e.card) : e.card : n.card = t.longs === String ? a.Long.prototype.toString.call(e.card) : t.longs === Number ? new a.LongBits(e.card.low >>> 0, e.card.high >>> 0).toNumber() : e.card);
null != e.coin && e.hasOwnProperty("coin") && ("number" == typeof e.coin ? n.coin = t.longs === String ? String(e.coin) : e.coin : n.coin = t.longs === String ? a.Long.prototype.toString.call(e.coin) : t.longs === Number ? new a.LongBits(e.coin.low >>> 0, e.coin.high >>> 0).toNumber() : e.coin);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
e.RoomInfo = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.roomId = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.prototype.password = "";
e.prototype.pay = 0;
e.prototype.round = 0;
e.prototype.exitPay = !1;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(8).int64(e.roomId);
t.uint32(18).string(e.password);
t.uint32(24).int32(e.pay);
t.uint32(32).int32(e.round);
t.uint32(40).bool(e.exitPay);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.RoomInfo(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.roomId = e.int64();
break;

case 2:
r.password = e.string();
break;

case 3:
r.pay = e.int32();
break;

case 4:
r.round = e.int32();
break;

case 5:
r.exitPay = e.bool();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("roomId")) throw a.ProtocolError("missing required 'roomId'", {
instance: r
});
if (!r.hasOwnProperty("password")) throw a.ProtocolError("missing required 'password'", {
instance: r
});
if (!r.hasOwnProperty("pay")) throw a.ProtocolError("missing required 'pay'", {
instance: r
});
if (!r.hasOwnProperty("round")) throw a.ProtocolError("missing required 'round'", {
instance: r
});
if (!r.hasOwnProperty("exitPay")) throw a.ProtocolError("missing required 'exitPay'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
if ("object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e) return "object expected";
if (!(a.isInteger(e.roomId) || e.roomId && a.isInteger(e.roomId.low) && a.isInteger(e.roomId.high))) return "roomId: integer|Long expected";
if (!a.isString(e.password)) return "password: string expected";
switch (e.pay) {
default:
return "pay: enum value expected";

case 0:
case 1:
}
switch (e.round) {
default:
return "round: enum value expected";

case 0:
case 1:
}
return "boolean" != typeof e.exitPay ? "exitPay: boolean expected" : null;
};
e.fromObject = function(e) {
if (e instanceof u.pb.RoomInfo) return e;
var t = new u.pb.RoomInfo();
null != e.roomId && (a.Long ? (t.roomId = a.Long.fromValue(e.roomId)).unsigned = !1 : "string" == typeof e.roomId ? t.roomId = parseInt(e.roomId, 10) : "number" == typeof e.roomId ? t.roomId = e.roomId : "object" === r(e.roomId) && (t.roomId = new a.LongBits(e.roomId.low >>> 0, e.roomId.high >>> 0).toNumber()));
null != e.password && (t.password = String(e.password));
switch (e.pay) {
case "SHARE":
case 0:
t.pay = 0;
break;

case "OWNER":
case 1:
t.pay = 1;
}
switch (e.round) {
case "ROUND4":
case 0:
t.round = 0;
break;

case "ROUND8":
case 1:
t.round = 1;
}
null != e.exitPay && (t.exitPay = Boolean(e.exitPay));
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
if (a.Long) {
var r = new a.Long(0, 0, !1);
n.roomId = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.roomId = t.longs === String ? "0" : 0;
n.password = "";
n.pay = t.enums === String ? "SHARE" : 0;
n.round = t.enums === String ? "ROUND4" : 0;
n.exitPay = !1;
}
null != e.roomId && e.hasOwnProperty("roomId") && ("number" == typeof e.roomId ? n.roomId = t.longs === String ? String(e.roomId) : e.roomId : n.roomId = t.longs === String ? a.Long.prototype.toString.call(e.roomId) : t.longs === Number ? new a.LongBits(e.roomId.low >>> 0, e.roomId.high >>> 0).toNumber() : e.roomId);
null != e.password && e.hasOwnProperty("password") && (n.password = e.password);
null != e.pay && e.hasOwnProperty("pay") && (n.pay = t.enums === String ? u.pb.PayMode[e.pay] : e.pay);
null != e.round && e.hasOwnProperty("round") && (n.round = t.enums === String ? u.pb.GameRound[e.round] : e.round);
null != e.exitPay && e.hasOwnProperty("exitPay") && (n.exitPay = e.exitPay);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
e.C2GSLogin = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.user = "";
e.prototype.password = "";
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(10).string(e.user);
t.uint32(18).string(e.password);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.C2GSLogin(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.user = e.string();
break;

case 2:
r.password = e.string();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("user")) throw a.ProtocolError("missing required 'user'", {
instance: r
});
if (!r.hasOwnProperty("password")) throw a.ProtocolError("missing required 'password'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
return "object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e ? "object expected" : a.isString(e.user) ? a.isString(e.password) ? null : "password: string expected" : "user: string expected";
};
e.fromObject = function(e) {
if (e instanceof u.pb.C2GSLogin) return e;
var t = new u.pb.C2GSLogin();
null != e.user && (t.user = String(e.user));
null != e.password && (t.password = String(e.password));
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
n.user = "";
n.password = "";
}
null != e.user && e.hasOwnProperty("user") && (n.user = e.user);
null != e.password && e.hasOwnProperty("password") && (n.password = e.password);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
e.GS2CLoginRet = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.user = null;
e.prototype.errorCode = 1;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
u.pb.PlayerInfo.encode(e.user, t.uint32(10).fork()).ldelim();
t.uint32(16).int32(e.errorCode);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.GS2CLoginRet(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.user = u.pb.PlayerInfo.decode(e, e.uint32());
break;

case 2:
r.errorCode = e.int32();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("user")) throw a.ProtocolError("missing required 'user'", {
instance: r
});
if (!r.hasOwnProperty("errorCode")) throw a.ProtocolError("missing required 'errorCode'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
if ("object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e) return "object expected";
var t = u.pb.PlayerInfo.verify(e.user);
if (t) return "user." + t;
switch (e.errorCode) {
default:
return "errorCode: enum value expected";

case 1:
case 2:
}
return null;
};
e.fromObject = function(e) {
if (e instanceof u.pb.GS2CLoginRet) return e;
var t = new u.pb.GS2CLoginRet();
if (null != e.user) {
if ("object" !== r(e.user)) throw TypeError(".pb.GS2CLoginRet.user: object expected");
t.user = u.pb.PlayerInfo.fromObject(e.user);
}
switch (e.errorCode) {
case "Success":
case 1:
t.errorCode = 1;
break;

case "Fail":
case 2:
t.errorCode = 2;
}
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
n.user = null;
n.errorCode = t.enums === String ? "Success" : 1;
}
null != e.user && e.hasOwnProperty("user") && (n.user = u.pb.PlayerInfo.toObject(e.user, t));
null != e.errorCode && e.hasOwnProperty("errorCode") && (n.errorCode = t.enums === String ? u.pb.GS2CLoginRet.ErrorCode[e.errorCode] : e.errorCode);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
e.ErrorCode = function() {
var e = {}, t = Object.create(e);
t[e[1] = "Success"] = 1;
t[e[2] = "Fail"] = 2;
return t;
}();
return e;
}();
e.C2GSCreateRoom = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.pay = 0;
e.prototype.round = 0;
e.prototype.exitPay = !1;
e.prototype.password = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(8).int32(e.pay);
t.uint32(16).int32(e.round);
t.uint32(24).bool(e.exitPay);
t.uint32(32).int64(e.password);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.C2GSCreateRoom(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.pay = e.int32();
break;

case 2:
r.round = e.int32();
break;

case 3:
r.exitPay = e.bool();
break;

case 4:
r.password = e.int64();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("pay")) throw a.ProtocolError("missing required 'pay'", {
instance: r
});
if (!r.hasOwnProperty("round")) throw a.ProtocolError("missing required 'round'", {
instance: r
});
if (!r.hasOwnProperty("exitPay")) throw a.ProtocolError("missing required 'exitPay'", {
instance: r
});
if (!r.hasOwnProperty("password")) throw a.ProtocolError("missing required 'password'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
if ("object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e) return "object expected";
switch (e.pay) {
default:
return "pay: enum value expected";

case 0:
case 1:
}
switch (e.round) {
default:
return "round: enum value expected";

case 0:
case 1:
}
return "boolean" != typeof e.exitPay ? "exitPay: boolean expected" : a.isInteger(e.password) || e.password && a.isInteger(e.password.low) && a.isInteger(e.password.high) ? null : "password: integer|Long expected";
};
e.fromObject = function(e) {
if (e instanceof u.pb.C2GSCreateRoom) return e;
var t = new u.pb.C2GSCreateRoom();
switch (e.pay) {
case "SHARE":
case 0:
t.pay = 0;
break;

case "OWNER":
case 1:
t.pay = 1;
}
switch (e.round) {
case "ROUND4":
case 0:
t.round = 0;
break;

case "ROUND8":
case 1:
t.round = 1;
}
null != e.exitPay && (t.exitPay = Boolean(e.exitPay));
null != e.password && (a.Long ? (t.password = a.Long.fromValue(e.password)).unsigned = !1 : "string" == typeof e.password ? t.password = parseInt(e.password, 10) : "number" == typeof e.password ? t.password = e.password : "object" === r(e.password) && (t.password = new a.LongBits(e.password.low >>> 0, e.password.high >>> 0).toNumber()));
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
n.pay = t.enums === String ? "SHARE" : 0;
n.round = t.enums === String ? "ROUND4" : 0;
n.exitPay = !1;
if (a.Long) {
var r = new a.Long(0, 0, !1);
n.password = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.password = t.longs === String ? "0" : 0;
}
null != e.pay && e.hasOwnProperty("pay") && (n.pay = t.enums === String ? u.pb.PayMode[e.pay] : e.pay);
null != e.round && e.hasOwnProperty("round") && (n.round = t.enums === String ? u.pb.GameRound[e.round] : e.round);
null != e.exitPay && e.hasOwnProperty("exitPay") && (n.exitPay = e.exitPay);
null != e.password && e.hasOwnProperty("password") && ("number" == typeof e.password ? n.password = t.longs === String ? String(e.password) : e.password : n.password = t.longs === String ? a.Long.prototype.toString.call(e.password) : t.longs === Number ? new a.LongBits(e.password.low >>> 0, e.password.high >>> 0).toNumber() : e.password);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
e.C2GSEnterRoom = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.roomId = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.prototype.password = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
null != e.roomId && e.hasOwnProperty("roomId") && t.uint32(8).int64(e.roomId);
null != e.password && e.hasOwnProperty("password") && t.uint32(16).int64(e.password);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.C2GSEnterRoom(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.roomId = e.int64();
break;

case 2:
r.password = e.int64();
break;

default:
e.skipType(7 & o);
}
}
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
return "object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e ? "object expected" : null != e.roomId && e.hasOwnProperty("roomId") && !(a.isInteger(e.roomId) || e.roomId && a.isInteger(e.roomId.low) && a.isInteger(e.roomId.high)) ? "roomId: integer|Long expected" : null != e.password && e.hasOwnProperty("password") && !(a.isInteger(e.password) || e.password && a.isInteger(e.password.low) && a.isInteger(e.password.high)) ? "password: integer|Long expected" : null;
};
e.fromObject = function(e) {
if (e instanceof u.pb.C2GSEnterRoom) return e;
var t = new u.pb.C2GSEnterRoom();
null != e.roomId && (a.Long ? (t.roomId = a.Long.fromValue(e.roomId)).unsigned = !1 : "string" == typeof e.roomId ? t.roomId = parseInt(e.roomId, 10) : "number" == typeof e.roomId ? t.roomId = e.roomId : "object" === r(e.roomId) && (t.roomId = new a.LongBits(e.roomId.low >>> 0, e.roomId.high >>> 0).toNumber()));
null != e.password && (a.Long ? (t.password = a.Long.fromValue(e.password)).unsigned = !1 : "string" == typeof e.password ? t.password = parseInt(e.password, 10) : "number" == typeof e.password ? t.password = e.password : "object" === r(e.password) && (t.password = new a.LongBits(e.password.low >>> 0, e.password.high >>> 0).toNumber()));
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
if (a.Long) {
var r = new a.Long(0, 0, !1);
n.roomId = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.roomId = t.longs === String ? "0" : 0;
if (a.Long) {
r = new a.Long(0, 0, !1);
n.password = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.password = t.longs === String ? "0" : 0;
}
null != e.roomId && e.hasOwnProperty("roomId") && ("number" == typeof e.roomId ? n.roomId = t.longs === String ? String(e.roomId) : e.roomId : n.roomId = t.longs === String ? a.Long.prototype.toString.call(e.roomId) : t.longs === Number ? new a.LongBits(e.roomId.low >>> 0, e.roomId.high >>> 0).toNumber() : e.roomId);
null != e.password && e.hasOwnProperty("password") && ("number" == typeof e.password ? n.password = t.longs === String ? String(e.password) : e.password : n.password = t.longs === String ? a.Long.prototype.toString.call(e.password) : t.longs === Number ? new a.LongBits(e.password.low >>> 0, e.password.high >>> 0).toNumber() : e.password);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
e.GS2CEnterRoomRet = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.roomId = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.prototype.rountIndex = 0;
e.prototype.errorCode = 1;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(8).int64(e.roomId);
t.uint32(16).int32(e.rountIndex);
t.uint32(24).int32(e.errorCode);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.GS2CEnterRoomRet(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.roomId = e.int64();
break;

case 2:
r.rountIndex = e.int32();
break;

case 3:
r.errorCode = e.int32();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("roomId")) throw a.ProtocolError("missing required 'roomId'", {
instance: r
});
if (!r.hasOwnProperty("rountIndex")) throw a.ProtocolError("missing required 'rountIndex'", {
instance: r
});
if (!r.hasOwnProperty("errorCode")) throw a.ProtocolError("missing required 'errorCode'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
if ("object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e) return "object expected";
if (!(a.isInteger(e.roomId) || e.roomId && a.isInteger(e.roomId.low) && a.isInteger(e.roomId.high))) return "roomId: integer|Long expected";
if (!a.isInteger(e.rountIndex)) return "rountIndex: integer expected";
switch (e.errorCode) {
default:
return "errorCode: enum value expected";

case 1:
case 2:
case 3:
case 4:
}
return null;
};
e.fromObject = function(e) {
if (e instanceof u.pb.GS2CEnterRoomRet) return e;
var t = new u.pb.GS2CEnterRoomRet();
null != e.roomId && (a.Long ? (t.roomId = a.Long.fromValue(e.roomId)).unsigned = !1 : "string" == typeof e.roomId ? t.roomId = parseInt(e.roomId, 10) : "number" == typeof e.roomId ? t.roomId = e.roomId : "object" === r(e.roomId) && (t.roomId = new a.LongBits(e.roomId.low >>> 0, e.roomId.high >>> 0).toNumber()));
null != e.rountIndex && (t.rountIndex = 0 | e.rountIndex);
switch (e.errorCode) {
case "Success":
case 1:
t.errorCode = 1;
break;

case "Fail":
case 2:
t.errorCode = 2;
break;

case "GameStart":
case 3:
t.errorCode = 3;
break;

case "NeedPassword":
case 4:
t.errorCode = 4;
}
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
if (a.Long) {
var r = new a.Long(0, 0, !1);
n.roomId = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.roomId = t.longs === String ? "0" : 0;
n.rountIndex = 0;
n.errorCode = t.enums === String ? "Success" : 1;
}
null != e.roomId && e.hasOwnProperty("roomId") && ("number" == typeof e.roomId ? n.roomId = t.longs === String ? String(e.roomId) : e.roomId : n.roomId = t.longs === String ? a.Long.prototype.toString.call(e.roomId) : t.longs === Number ? new a.LongBits(e.roomId.low >>> 0, e.roomId.high >>> 0).toNumber() : e.roomId);
null != e.rountIndex && e.hasOwnProperty("rountIndex") && (n.rountIndex = e.rountIndex);
null != e.errorCode && e.hasOwnProperty("errorCode") && (n.errorCode = t.enums === String ? u.pb.GS2CEnterRoomRet.ErrorCode[e.errorCode] : e.errorCode);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
e.ErrorCode = function() {
var e = {}, t = Object.create(e);
t[e[1] = "Success"] = 1;
t[e[2] = "Fail"] = 2;
t[e[3] = "GameStart"] = 3;
t[e[4] = "NeedPassword"] = 4;
return t;
}();
return e;
}();
e.GS2CTurnToBet = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.GS2CTurnToBet(); e.pos < n; ) {
var o = e.uint32();
e.skipType(7 & o);
}
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
return "object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e ? "object expected" : null;
};
e.fromObject = function(e) {
return e instanceof u.pb.GS2CTurnToBet ? e : new u.pb.GS2CTurnToBet();
};
e.toObject = function() {
return {};
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
e.C2GSBet = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.roomId = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.prototype.rountIndex = 0;
e.prototype.betSide = 0;
e.prototype.bet = a.Long ? a.Long.fromBits(0, 0, !1) : 0;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(8).int64(e.roomId);
t.uint32(16).int32(e.rountIndex);
t.uint32(24).int32(e.betSide);
t.uint32(32).int64(e.bet);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.C2GSBet(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.roomId = e.int64();
break;

case 2:
r.rountIndex = e.int32();
break;

case 3:
r.betSide = e.int32();
break;

case 4:
r.bet = e.int64();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("roomId")) throw a.ProtocolError("missing required 'roomId'", {
instance: r
});
if (!r.hasOwnProperty("rountIndex")) throw a.ProtocolError("missing required 'rountIndex'", {
instance: r
});
if (!r.hasOwnProperty("betSide")) throw a.ProtocolError("missing required 'betSide'", {
instance: r
});
if (!r.hasOwnProperty("bet")) throw a.ProtocolError("missing required 'bet'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
return "object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e ? "object expected" : a.isInteger(e.roomId) || e.roomId && a.isInteger(e.roomId.low) && a.isInteger(e.roomId.high) ? a.isInteger(e.rountIndex) ? a.isInteger(e.betSide) ? a.isInteger(e.bet) || e.bet && a.isInteger(e.bet.low) && a.isInteger(e.bet.high) ? null : "bet: integer|Long expected" : "betSide: integer expected" : "rountIndex: integer expected" : "roomId: integer|Long expected";
};
e.fromObject = function(e) {
if (e instanceof u.pb.C2GSBet) return e;
var t = new u.pb.C2GSBet();
null != e.roomId && (a.Long ? (t.roomId = a.Long.fromValue(e.roomId)).unsigned = !1 : "string" == typeof e.roomId ? t.roomId = parseInt(e.roomId, 10) : "number" == typeof e.roomId ? t.roomId = e.roomId : "object" === r(e.roomId) && (t.roomId = new a.LongBits(e.roomId.low >>> 0, e.roomId.high >>> 0).toNumber()));
null != e.rountIndex && (t.rountIndex = 0 | e.rountIndex);
null != e.betSide && (t.betSide = 0 | e.betSide);
null != e.bet && (a.Long ? (t.bet = a.Long.fromValue(e.bet)).unsigned = !1 : "string" == typeof e.bet ? t.bet = parseInt(e.bet, 10) : "number" == typeof e.bet ? t.bet = e.bet : "object" === r(e.bet) && (t.bet = new a.LongBits(e.bet.low >>> 0, e.bet.high >>> 0).toNumber()));
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
if (a.Long) {
var r = new a.Long(0, 0, !1);
n.roomId = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.roomId = t.longs === String ? "0" : 0;
n.rountIndex = 0;
n.betSide = 0;
if (a.Long) {
r = new a.Long(0, 0, !1);
n.bet = t.longs === String ? r.toString() : t.longs === Number ? r.toNumber() : r;
} else n.bet = t.longs === String ? "0" : 0;
}
null != e.roomId && e.hasOwnProperty("roomId") && ("number" == typeof e.roomId ? n.roomId = t.longs === String ? String(e.roomId) : e.roomId : n.roomId = t.longs === String ? a.Long.prototype.toString.call(e.roomId) : t.longs === Number ? new a.LongBits(e.roomId.low >>> 0, e.roomId.high >>> 0).toNumber() : e.roomId);
null != e.rountIndex && e.hasOwnProperty("rountIndex") && (n.rountIndex = e.rountIndex);
null != e.betSide && e.hasOwnProperty("betSide") && (n.betSide = e.betSide);
null != e.bet && e.hasOwnProperty("bet") && ("number" == typeof e.bet ? n.bet = t.longs === String ? String(e.bet) : e.bet : n.bet = t.longs === String ? a.Long.prototype.toString.call(e.bet) : t.longs === Number ? new a.LongBits(e.bet.low >>> 0, e.bet.high >>> 0).toNumber() : e.bet);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
e.GS2CBetRet = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.errorCode = 1;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(8).int32(e.errorCode);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.GS2CBetRet(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.errorCode = e.int32();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("errorCode")) throw a.ProtocolError("missing required 'errorCode'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
if ("object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e) return "object expected";
switch (e.errorCode) {
default:
return "errorCode: enum value expected";

case 1:
case 2:
case 3:
case 4:
}
return null;
};
e.fromObject = function(e) {
if (e instanceof u.pb.GS2CBetRet) return e;
var t = new u.pb.GS2CBetRet();
switch (e.errorCode) {
case "Success":
case 1:
t.errorCode = 1;
break;

case "IndexError":
case 2:
t.errorCode = 2;
break;

case "CoinLess":
case 3:
t.errorCode = 3;
break;

case "Fail":
case 4:
t.errorCode = 4;
}
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
t.defaults && (n.errorCode = t.enums === String ? "Success" : 1);
null != e.errorCode && e.hasOwnProperty("errorCode") && (n.errorCode = t.enums === String ? u.pb.GS2CBetRet.ErrorCode[e.errorCode] : e.errorCode);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
e.ErrorCode = function() {
var e = {}, t = Object.create(e);
t[e[1] = "Success"] = 1;
t[e[2] = "IndexError"] = 2;
t[e[3] = "CoinLess"] = 3;
t[e[4] = "Fail"] = 4;
return t;
}();
return e;
}();
e.GS2CNewRoundStart = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.rountIndex = 0;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(8).int32(e.rountIndex);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.GS2CNewRoundStart(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.rountIndex = e.int32();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("rountIndex")) throw a.ProtocolError("missing required 'rountIndex'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
return "object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e ? "object expected" : a.isInteger(e.rountIndex) ? null : "rountIndex: integer expected";
};
e.fromObject = function(e) {
if (e instanceof u.pb.GS2CNewRoundStart) return e;
var t = new u.pb.GS2CNewRoundStart();
null != e.rountIndex && (t.rountIndex = 0 | e.rountIndex);
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
t.defaults && (n.rountIndex = 0);
null != e.rountIndex && e.hasOwnProperty("rountIndex") && (n.rountIndex = e.rountIndex);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
e.GS2CGameResults = function() {
function e(e) {
if (e) for (var t = Object.keys(e), n = 0; n < t.length; ++n) null != e[t[n]] && (this[t[n]] = e[t[n]]);
}
e.prototype.rountIndex = 0;
e.prototype.results = !1;
e.create = function(t) {
return new e(t);
};
e.encode = function(e, t) {
t || (t = s.create());
t.uint32(8).int32(e.rountIndex);
t.uint32(16).bool(e.results);
return t;
};
e.encodeDelimited = function(e, t) {
return this.encode(e, t).ldelim();
};
e.decode = function(e, t) {
e instanceof i || (e = i.create(e));
for (var n = void 0 === t ? e.len : e.pos + t, r = new u.pb.GS2CGameResults(); e.pos < n; ) {
var o = e.uint32();
switch (o >>> 3) {
case 1:
r.rountIndex = e.int32();
break;

case 2:
r.results = e.bool();
break;

default:
e.skipType(7 & o);
}
}
if (!r.hasOwnProperty("rountIndex")) throw a.ProtocolError("missing required 'rountIndex'", {
instance: r
});
if (!r.hasOwnProperty("results")) throw a.ProtocolError("missing required 'results'", {
instance: r
});
return r;
};
e.decodeDelimited = function(e) {
e instanceof i || (e = new i(e));
return this.decode(e, e.uint32());
};
e.verify = function(e) {
return "object" !== ("undefined" == typeof e ? "undefined" : r(e)) || null === e ? "object expected" : a.isInteger(e.rountIndex) ? "boolean" != typeof e.results ? "results: boolean expected" : null : "rountIndex: integer expected";
};
e.fromObject = function(e) {
if (e instanceof u.pb.GS2CGameResults) return e;
var t = new u.pb.GS2CGameResults();
null != e.rountIndex && (t.rountIndex = 0 | e.rountIndex);
null != e.results && (t.results = Boolean(e.results));
return t;
};
e.toObject = function(e, t) {
t || (t = {});
var n = {};
if (t.defaults) {
n.rountIndex = 0;
n.results = !1;
}
null != e.rountIndex && e.hasOwnProperty("rountIndex") && (n.rountIndex = e.rountIndex);
null != e.results && e.hasOwnProperty("results") && (n.results = e.results);
return n;
};
e.prototype.toJSON = function() {
return this.constructor.toObject(this, o.util.toJSONOptions);
};
return e;
}();
return e;
}();
t.exports = u;
cc._RF.pop();
}, {
"protobufjs/minimal": 8
} ],
playerInfo: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "d3c25S6eIdGaKTH1rikLbOm", "playerInfo");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = e("../player/playerManager"), o = cc._decorator, i = o.ccclass, s = (o.property, 
function(e) {
__extends(t, e);
function t() {
return null !== e && e.apply(this, arguments) || this;
}
t.prototype.onLoad = function() {
this.mName = cc.find("nameContainer/name", this.node).getComponent(cc.Label);
this.mCard = cc.find("cardContainer/card", this.node).getComponent(cc.Label);
this.mCoin = cc.find("coinContaner/coin", this.node).getComponent(cc.Label);
this.mButtonCharge = cc.find("button_charge", this.node).getComponent(cc.Button);
this.mButtonCharge.node.on("click", this.onClickCharge, this);
};
t.prototype.start = function() {
this.initUI();
};
t.prototype.initUI = function() {
var e = r.PlayerManager.getInstance().mMy;
this.mName.string = e.nickname;
this.mCard.string = e.card.toString();
this.mCoin.string = e.coin.toString();
};
t.prototype.onClickCharge = function(e) {
console.log("onClickCharge");
};
return t = __decorate([ i ], t);
}(cc.Component));
n.PlayerInfoNode = s;
cc._RF.pop();
}, {
"../player/playerManager": "playerManager"
} ],
playerManager: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "91ecdZhek9NDIoQTNLJxHNx", "playerManager");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = e("./player"), o = function() {
function e() {
this.mMy = new r.Player();
}
e.getInstance = function() {
null == e.instance && (e.instance = new e());
return e.instance;
};
e.prototype.initMy = function(e) {
this.mMy.newPlayer(e);
};
e.instance = null;
return e;
}();
n.PlayerManager = o;
cc._RF.pop();
}, {
"./player": "player"
} ],
player: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "d2697+T6URKWL96vqPOMCj5", "player");
Object.defineProperty(n, "__esModule", {
value: !0
});
(function(e) {
e[e.Idle = 0] = "Idle";
e[e.Bet = 1] = "Bet";
e[e.WaitingBet = 2] = "WaitingBet";
e[e.Over = 3] = "Over";
})(n.GameState || (n.GameState = {}));
var r = function() {
function e() {
this.mId = 0;
this.nickname = "";
this.headicon = "";
this.card = 0;
this.coin = 0;
}
e.prototype.newPlayer = function(e) {
this.mId = e.playerId;
this.nickname = e.nickname;
this.headicon = e.headicon;
this.card = e.card;
this.coin = e.coin;
};
e.prototype.updateRoomId = function(e) {
this.roomId = e;
};
e.prototype.getRoomId = function() {
return this.roomId;
};
e.prototype.updateRoomRoundIndex = function(e) {
this.roundIndex = e;
};
e.prototype.getRoomRoundIndex = function() {
return this.roundIndex;
};
e.prototype.updateGameState = function(e) {
this.gameState = e;
};
e.prototype.getGameState = function() {
return this.gameState;
};
return e;
}();
n.Player = r;
cc._RF.pop();
}, {} ],
room: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "9c545yBXTBG0KLcgH9f6KT/", "room");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = e("../handler/EventDispatch"), o = e("../handler/EventType"), i = e("../player/playerManager"), s = e("../uimanager/uimanager"), a = e("../player/player"), u = cc._decorator, c = u.ccclass, l = (u.property, 
function(e) {
__extends(t, e);
function t() {
return null !== e && e.apply(this, arguments) || this;
}
t.prototype.onLoad = function() {
this.roomId = cc.find("table/room_num/roomId", this.node).getComponent(cc.Label);
this.roundIndex = cc.find("table/title/roundIndex", this.node).getComponent(cc.Label);
this.blue_bet_bar = cc.find("betContainer/betBar/betBar_blue", this.node).getComponent(cc.Sprite);
this.blue_bet_value = cc.find("betContainer/betBar/betBar_blue/blue_value", this.node).getComponent(cc.Label);
this.button_blue_bet = cc.find("betContainer/betBar/betBar_blue/buttonBet_blue", this.node).getComponent(cc.Button);
this.red_bet_bar = cc.find("betContainer/betBar/betBar_red", this.node).getComponent(cc.Sprite);
this.red_bet_value = cc.find("betContainer/betBar/betBar_blue/red_value", this.node).getComponent(cc.Label);
this.button_red_bet = cc.find("betContainer/betBar/betBar_blue/buttonBet_red", this.node).getComponent(cc.Button);
s.UIManager.Instance.showPlayerInfoNode(!0);
};
t.prototype.start = function() {
r.EventDispatch.register(o.EventType.ROOM_UPDATE_ROUND_INDEX, this.updateRoundIndex, null);
this.initUI();
};
t.prototype.initUI = function() {
this.roomId.string = String(i.PlayerManager.getInstance().mMy.getRoomId());
this.updateRoundIndex();
i.PlayerManager.getInstance().mMy.getGameState() == a.GameState.Bet && this.showTimer();
};
t.prototype.showTimer = function() {};
t.prototype.updateRoundIndex = function() {
var e = i.PlayerManager.getInstance().mMy.getRoomRoundIndex();
this.roundIndex.string = this.getChineseWords(e);
};
t.prototype.getChineseWords = function(e) {
for (var t = String(e), n = "", r = 0; r < t.length; r++) "1" == t[r] ? n += "一" : "2" == t[r] ? n += "二" : "3" == t[r] ? n += "三" : "4" == t[r] ? n += "四" : "5" == t[r] ? n += "五" : "6" == t[r] ? n += "六" : "7" == t[r] ? n += "七" : "8" == t[r] ? n += "八" : "9" == t[r] && (n += "九");
return n;
};
return t = __decorate([ c ], t);
}(cc.Component));
n.Room = l;
cc._RF.pop();
}, {
"../handler/EventDispatch": "EventDispatch",
"../handler/EventType": "EventType",
"../player/player": "player",
"../player/playerManager": "playerManager",
"../uimanager/uimanager": "uimanager"
} ],
uimanager: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "d19cbj7GTFElq069hwjHN61", "uimanager");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r = cc._decorator, o = r.ccclass, i = (r.property, e("./windowDefine")), s = e("./../myTool/myTool"), a = function(e) {
__extends(t, e);
function t() {
var t = null !== e && e.apply(this, arguments) || this;
t.mPlayerInfoNode = null;
return t;
}
n = t;
t.prototype.onLoad = function() {
n.Instance = this;
this.curWindowId = i.WindowId.Init;
this.initPlayerInfo();
};
t.prototype.initPlayerInfo = function() {
var e = i.ResManager.getPrefabPath(i.WindowId.PlayerInfo);
"" != e && s.MyTool.AddChild(this.node, e);
};
t.prototype.start = function() {};
t.prototype.showWindow = function(e) {
console.log("showWindow[" + e + "]");
if (this.curWindowId != e) {
var t = i.ResManager.getPrefabPath(e);
if ("" != t) {
s.MyTool.AddChild(this.node, t);
var n = cc.find(this.curWindowId, this.node);
null != n ? n.destroy() : console.error("curWindow[", this.curWindowId, "] can't find");
this.curWindowId = e;
}
} else console.log("window[" + e + "] has been shown.");
};
t.prototype.showPlayerInfoNode = function(e) {
console.log("showPlayerInfoNode:", e);
null == this.mPlayerInfoNode && (this.mPlayerInfoNode = cc.find(i.WindowId.PlayerInfo, this.node));
this.mPlayerInfoNode.active = e;
this.mPlayerInfoNode.zIndex = e ? 1 : 0;
};
t.prototype.update = function(e) {};
var n;
return t = n = __decorate([ o ], t);
}(cc.Component);
n.UIManager = a;
cc._RF.pop();
}, {
"./../myTool/myTool": "myTool",
"./windowDefine": "windowDefine"
} ],
windowDefine: [ function(e, t, n) {
"use strict";
cc._RF.push(t, "65aa6EV1k9AQqgZ8P2aF5W1", "windowDefine");
Object.defineProperty(n, "__esModule", {
value: !0
});
var r;
(function(e) {
e.Init = "init";
e.Login = "login";
e.PlayerInfo = "playerInfo";
e.Hall = "hall";
e.Room = "room";
})(r = n.WindowId || (n.WindowId = {}));
var o = function() {
function e() {}
e.getPrefabPath = function(e) {
switch (e) {
case r.Login:
return "Prefabs/login";

case r.Hall:
return "Prefabs/hall";

case r.Room:
return "Prefabs/room";

case r.PlayerInfo:
return "Prefabs/playerInfo";

default:
return "";
}
};
return e;
}();
n.ResManager = o;
cc._RF.pop();
}, {} ]
}, {}, [ "EventDispatch", "EventType", "Observer", "myTool", "NetManager", "Network", "messageID", "pb", "player", "playerManager", "gameMessageHandler", "hall", "login", "playerInfo", "room", "uimanager", "windowDefine" ]);