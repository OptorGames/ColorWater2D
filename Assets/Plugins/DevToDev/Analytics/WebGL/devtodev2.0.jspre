(function (scope) {
    function ownKeys(t, e) {
        var r = Object.keys(t);
        return Object.getOwnPropertySymbols && r.push.apply(r, Object.getOwnPropertySymbols(t)), e && (r = r.filter(function (e) {
            return Object.getOwnPropertyDescriptor(t, e).enumerable
        })), r
    }

    function _objectSpread(t) {
        for (var r = 1; r < arguments.length; r++) if (r % 2) {
            var n = null != arguments[r] ? arguments[r] : {};
            ownKeys(n, !0).forEach(function (e) {
                _defineProperty(t, e, n[e])
            })
        } else Object.getOwnPropertyDescriptors ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(arguments[r])) : ownKeys(n).forEach(function (e) {
            Object.defineProperty(t, e, Object.getOwnPropertyDescriptor(arguments[r], e))
        });
        return t
    }

    function _defineProperty(e, t, r) {
        return t in e ? Object.defineProperty(e, t, {
            value: r,
            enumerable: !0,
            configurable: !0,
            writable: !0
        }) : e[t] = r, e
    }

    function _typeof(e) {
        return (_typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (e) {
            return typeof e
        } : function (e) {
            return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
        })(e)
    }

    !function (e) {
        if ("object" === ("undefined" == typeof exports ? "undefined" : _typeof(exports)) && "undefined" != typeof module) module.exports = e(); else if ("function" == typeof define && define.amd) define([], e); else {
            ("undefined" != typeof window ? window : "undefined" != typeof global ? global : "undefined" != typeof self ? self : this).iampakopako = e()
        }
    }(function () {
        return function i(a, s, u) {
            function c(t, e) {
                if (!s[t]) {
                    if (!a[t]) {
                        var r = "function" == typeof require && require;
                        if (!e && r) return r(t, !0);
                        if (l) return l(t, !0);
                        var n = new Error("Cannot find module '" + t + "'");
                        throw n.code = "MODULE_NOT_FOUND", n
                    }
                    var o = s[t] = {exports: {}};
                    a[t][0].call(o.exports, function (e) {
                        return c(a[t][1][e] || e)
                    }, o, o.exports, i, a, s, u)
                }
                return s[t].exports
            }

            for (var l = "function" == typeof require && require, e = 0; e < u.length; e++) c(u[e]);
            return c
        }({
            1: [function (e, t, r) {
                "use strict";
                var n = "undefined" != typeof Uint8Array && "undefined" != typeof Uint16Array && "undefined" != typeof Int32Array;
                r.assign = function (e) {
                    for (var t, r, n = Array.prototype.slice.call(arguments, 1); n.length;) {
                        var o = n.shift();
                        if (o) {
                            if ("object" !== _typeof(o)) throw new TypeError(o + "must be non-object");
                            for (var i in o) t = o, r = i, Object.prototype.hasOwnProperty.call(t, r) && (e[i] = o[i])
                        }
                    }
                    return e
                }, r.shrinkBuf = function (e, t) {
                    return e.length === t ? e : e.subarray ? e.subarray(0, t) : (e.length = t, e)
                };
                var o = {
                    arraySet: function (e, t, r, n, o) {
                        if (t.subarray && e.subarray) e.set(t.subarray(r, r + n), o); else for (var i = 0; i < n; i++) e[o + i] = t[r + i]
                    }, flattenChunks: function (e) {
                        var t, r, n, o, i, a;
                        for (t = n = 0, r = e.length; t < r; t++) n += e[t].length;
                        for (a = new Uint8Array(n), t = o = 0, r = e.length; t < r; t++) i = e[t], a.set(i, o), o += i.length;
                        return a
                    }
                }, i = {
                    arraySet: function (e, t, r, n, o) {
                        for (var i = 0; i < n; i++) e[o + i] = t[r + i]
                    }, flattenChunks: function (e) {
                        return [].concat.apply([], e)
                    }
                };
                r.setTyped = function (e) {
                    e ? (r.Buf8 = Uint8Array, r.Buf16 = Uint16Array, r.Buf32 = Int32Array, r.assign(r, o)) : (r.Buf8 = Array, r.Buf16 = Array, r.Buf32 = Array, r.assign(r, i))
                }, r.setTyped(n)
            }, {}],
            2: [function (e, t, r) {
                "use strict";
                var u = e("./common"), o = !0, i = !0;
                try {
                    String.fromCharCode.apply(null, [0])
                } catch (e) {
                    o = !1
                }
                try {
                    String.fromCharCode.apply(null, new Uint8Array(1))
                } catch (e) {
                    i = !1
                }
                for (var c = new u.Buf8(256), n = 0; n < 256; n++) c[n] = 252 <= n ? 6 : 248 <= n ? 5 : 240 <= n ? 4 : 224 <= n ? 3 : 192 <= n ? 2 : 1;

                function l(e, t) {
                    if (t < 65534 && (e.subarray && i || !e.subarray && o)) return String.fromCharCode.apply(null, u.shrinkBuf(e, t));
                    for (var r = "", n = 0; n < t; n++) r += String.fromCharCode(e[n]);
                    return r
                }

                c[254] = c[254] = 1, r.string2buf = function (e) {
                    var t, r, n, o, i, a = e.length, s = 0;
                    for (o = 0; o < a; o++) 55296 == (64512 & (r = e.charCodeAt(o))) && o + 1 < a && 56320 == (64512 & (n = e.charCodeAt(o + 1))) && (r = 65536 + (r - 55296 << 10) + (n - 56320), o++), s += r < 128 ? 1 : r < 2048 ? 2 : r < 65536 ? 3 : 4;
                    for (t = new u.Buf8(s), o = i = 0; i < s; o++) 55296 == (64512 & (r = e.charCodeAt(o))) && o + 1 < a && 56320 == (64512 & (n = e.charCodeAt(o + 1))) && (r = 65536 + (r - 55296 << 10) + (n - 56320), o++), r < 128 ? t[i++] = r : (r < 2048 ? t[i++] = 192 | r >>> 6 : (r < 65536 ? t[i++] = 224 | r >>> 12 : (t[i++] = 240 | r >>> 18, t[i++] = 128 | r >>> 12 & 63), t[i++] = 128 | r >>> 6 & 63), t[i++] = 128 | 63 & r);
                    return t
                }, r.buf2binstring = function (e) {
                    return l(e, e.length)
                }, r.binstring2buf = function (e) {
                    for (var t = new u.Buf8(e.length), r = 0, n = t.length; r < n; r++) t[r] = e.charCodeAt(r);
                    return t
                }, r.buf2string = function (e, t) {
                    var r, n, o, i, a = t || e.length, s = new Array(2 * a);
                    for (r = n = 0; r < a;) if ((o = e[r++]) < 128) s[n++] = o; else if (4 < (i = c[o])) s[n++] = 65533, r += i - 1; else {
                        for (o &= 2 === i ? 31 : 3 === i ? 15 : 7; 1 < i && r < a;) o = o << 6 | 63 & e[r++], i--;
                        1 < i ? s[n++] = 65533 : o < 65536 ? s[n++] = o : (o -= 65536, s[n++] = 55296 | o >> 10 & 1023, s[n++] = 56320 | 1023 & o)
                    }
                    return l(s, n)
                }, r.utf8border = function (e, t) {
                    var r;
                    for ((t = t || e.length) > e.length && (t = e.length), r = t - 1; 0 <= r && 128 == (192 & e[r]);) r--;
                    return r < 0 ? t : 0 === r ? t : r + c[e[r]] > t ? r : t
                }
            }, {"./common": 1}],
            3: [function (e, t, r) {
                "use strict";
                t.exports = function (e, t, r, n) {
                    for (var o = 65535 & e | 0, i = e >>> 16 & 65535 | 0, a = 0; 0 !== r;) {
                        for (r -= a = 2e3 < r ? 2e3 : r; i = i + (o = o + t[n++] | 0) | 0, --a;) ;
                        o %= 65521, i %= 65521
                    }
                    return o | i << 16 | 0
                }
            }, {}],
            4: [function (e, t, r) {
                "use strict";
                var s = function () {
                    for (var e, t = [], r = 0; r < 256; r++) {
                        e = r;
                        for (var n = 0; n < 8; n++) e = 1 & e ? 3988292384 ^ e >>> 1 : e >>> 1;
                        t[r] = e
                    }
                    return t
                }();
                t.exports = function (e, t, r, n) {
                    var o = s, i = n + r;
                    e ^= -1;
                    for (var a = n; a < i; a++) e = e >>> 8 ^ o[255 & (e ^ t[a])];
                    return -1 ^ e
                }
            }, {}],
            5: [function (e, t, r) {
                "use strict";
                var u, f = e("../utils/common"), c = e("./trees"), p = e("./adler32"), g = e("./crc32"),
                    n = e("./messages"), l = 0, d = 4, y = 0, v = -2, m = -1, h = 4, o = 2, D = 8, _ = 9, i = 286,
                    a = 30, s = 19, P = 2 * i + 1, T = 15, C = 3, S = 258, I = S + C + 1, b = 42, w = 113, k = 1, E = 2,
                    A = 3, U = 4;

                function N(e, t) {
                    return e.msg = n[t], t
                }

                function x(e) {
                    return (e << 1) - (4 < e ? 9 : 0)
                }

                function O(e) {
                    for (var t = e.length; 0 <= --t;) e[t] = 0
                }

                function L(e) {
                    var t = e.state, r = t.pending;
                    r > e.avail_out && (r = e.avail_out), 0 !== r && (f.arraySet(e.output, t.pending_buf, t.pending_out, r, e.next_out), e.next_out += r, t.pending_out += r, e.total_out += r, e.avail_out -= r, t.pending -= r, 0 === t.pending && (t.pending_out = 0))
                }

                function R(e, t) {
                    c._tr_flush_block(e, 0 <= e.block_start ? e.block_start : -1, e.strstart - e.block_start, t), e.block_start = e.strstart, L(e.strm)
                }

                function V(e, t) {
                    e.pending_buf[e.pending++] = t
                }

                function B(e, t) {
                    e.pending_buf[e.pending++] = t >>> 8 & 255, e.pending_buf[e.pending++] = 255 & t
                }

                function z(e, t) {
                    var r, n, o = e.max_chain_length, i = e.strstart, a = e.prev_length, s = e.nice_match,
                        u = e.strstart > e.w_size - I ? e.strstart - (e.w_size - I) : 0, c = e.window, l = e.w_mask,
                        d = e.prev, f = e.strstart + S, p = c[i + a - 1], g = c[i + a];
                    e.prev_length >= e.good_match && (o >>= 2), s > e.lookahead && (s = e.lookahead);
                    do {
                        if (c[(r = t) + a] === g && c[r + a - 1] === p && c[r] === c[i] && c[++r] === c[i + 1]) {
                            i += 2, r++;
                            do {
                            } while (c[++i] === c[++r] && c[++i] === c[++r] && c[++i] === c[++r] && c[++i] === c[++r] && c[++i] === c[++r] && c[++i] === c[++r] && c[++i] === c[++r] && c[++i] === c[++r] && i < f);
                            if (n = S - (f - i), i = f - S, a < n) {
                                if (e.match_start = t, s <= (a = n)) break;
                                p = c[i + a - 1], g = c[i + a]
                            }
                        }
                    } while ((t = d[t & l]) > u && 0 != --o);
                    return a <= e.lookahead ? a : e.lookahead
                }

                function K(e) {
                    var t, r, n, o, i, a, s, u, c, l, d = e.w_size;
                    do {
                        if (o = e.window_size - e.lookahead - e.strstart, e.strstart >= d + (d - I)) {
                            for (f.arraySet(e.window, e.window, d, d, 0), e.match_start -= d, e.strstart -= d, e.block_start -= d, t = r = e.hash_size; n = e.head[--t], e.head[t] = d <= n ? n - d : 0, --r;) ;
                            for (t = r = d; n = e.prev[--t], e.prev[t] = d <= n ? n - d : 0, --r;) ;
                            o += d
                        }
                        if (0 === e.strm.avail_in) break;
                        if (a = e.strm, s = e.window, u = e.strstart + e.lookahead, c = o, l = void 0, l = a.avail_in, c < l && (l = c), r = 0 === l ? 0 : (a.avail_in -= l, f.arraySet(s, a.input, a.next_in, l, u), 1 === a.state.wrap ? a.adler = p(a.adler, s, l, u) : 2 === a.state.wrap && (a.adler = g(a.adler, s, l, u)), a.next_in += l, a.total_in += l, l), e.lookahead += r, e.lookahead + e.insert >= C) for (i = e.strstart - e.insert, e.ins_h = e.window[i], e.ins_h = (e.ins_h << e.hash_shift ^ e.window[i + 1]) & e.hash_mask; e.insert && (e.ins_h = (e.ins_h << e.hash_shift ^ e.window[i + C - 1]) & e.hash_mask, e.prev[i & e.w_mask] = e.head[e.ins_h], e.head[e.ins_h] = i, i++, e.insert--, !(e.lookahead + e.insert < C));) ;
                    } while (e.lookahead < I && 0 !== e.strm.avail_in)
                }

                function M(e, t) {
                    for (var r, n; ;) {
                        if (e.lookahead < I) {
                            if (K(e), e.lookahead < I && t === l) return k;
                            if (0 === e.lookahead) break
                        }
                        if (r = 0, e.lookahead >= C && (e.ins_h = (e.ins_h << e.hash_shift ^ e.window[e.strstart + C - 1]) & e.hash_mask, r = e.prev[e.strstart & e.w_mask] = e.head[e.ins_h], e.head[e.ins_h] = e.strstart), 0 !== r && e.strstart - r <= e.w_size - I && (e.match_length = z(e, r)), e.match_length >= C) if (n = c._tr_tally(e, e.strstart - e.match_start, e.match_length - C), e.lookahead -= e.match_length, e.match_length <= e.max_lazy_match && e.lookahead >= C) {
                            for (e.match_length--; e.strstart++, e.ins_h = (e.ins_h << e.hash_shift ^ e.window[e.strstart + C - 1]) & e.hash_mask, r = e.prev[e.strstart & e.w_mask] = e.head[e.ins_h], e.head[e.ins_h] = e.strstart, 0 != --e.match_length;) ;
                            e.strstart++
                        } else e.strstart += e.match_length, e.match_length = 0, e.ins_h = e.window[e.strstart], e.ins_h = (e.ins_h << e.hash_shift ^ e.window[e.strstart + 1]) & e.hash_mask; else n = c._tr_tally(e, 0, e.window[e.strstart]), e.lookahead--, e.strstart++;
                        if (n && (R(e, !1), 0 === e.strm.avail_out)) return k
                    }
                    return e.insert = e.strstart < C - 1 ? e.strstart : C - 1, t === d ? (R(e, !0), 0 === e.strm.avail_out ? A : U) : e.last_lit && (R(e, !1), 0 === e.strm.avail_out) ? k : E
                }

                function j(e, t) {
                    for (var r, n, o; ;) {
                        if (e.lookahead < I) {
                            if (K(e), e.lookahead < I && t === l) return k;
                            if (0 === e.lookahead) break
                        }
                        if (r = 0, e.lookahead >= C && (e.ins_h = (e.ins_h << e.hash_shift ^ e.window[e.strstart + C - 1]) & e.hash_mask, r = e.prev[e.strstart & e.w_mask] = e.head[e.ins_h], e.head[e.ins_h] = e.strstart), e.prev_length = e.match_length, e.prev_match = e.match_start, e.match_length = C - 1, 0 !== r && e.prev_length < e.max_lazy_match && e.strstart - r <= e.w_size - I && (e.match_length = z(e, r), e.match_length <= 5 && (1 === e.strategy || e.match_length === C && 4096 < e.strstart - e.match_start) && (e.match_length = C - 1)), e.prev_length >= C && e.match_length <= e.prev_length) {
                            for (o = e.strstart + e.lookahead - C, n = c._tr_tally(e, e.strstart - 1 - e.prev_match, e.prev_length - C), e.lookahead -= e.prev_length - 1, e.prev_length -= 2; ++e.strstart <= o && (e.ins_h = (e.ins_h << e.hash_shift ^ e.window[e.strstart + C - 1]) & e.hash_mask, r = e.prev[e.strstart & e.w_mask] = e.head[e.ins_h], e.head[e.ins_h] = e.strstart), 0 != --e.prev_length;) ;
                            if (e.match_available = 0, e.match_length = C - 1, e.strstart++, n && (R(e, !1), 0 === e.strm.avail_out)) return k
                        } else if (e.match_available) {
                            if ((n = c._tr_tally(e, 0, e.window[e.strstart - 1])) && R(e, !1), e.strstart++, e.lookahead--, 0 === e.strm.avail_out) return k
                        } else e.match_available = 1, e.strstart++, e.lookahead--
                    }
                    return e.match_available && (n = c._tr_tally(e, 0, e.window[e.strstart - 1]), e.match_available = 0), e.insert = e.strstart < C - 1 ? e.strstart : C - 1, t === d ? (R(e, !0), 0 === e.strm.avail_out ? A : U) : e.last_lit && (R(e, !1), 0 === e.strm.avail_out) ? k : E
                }

                function F(e, t, r, n, o) {
                    this.good_length = e, this.max_lazy = t, this.nice_length = r, this.max_chain = n, this.func = o
                }

                function G() {
                    this.strm = null, this.status = 0, this.pending_buf = null, this.pending_buf_size = 0, this.pending_out = 0, this.pending = 0, this.wrap = 0, this.gzhead = null, this.gzindex = 0, this.method = D, this.last_flush = -1, this.w_size = 0, this.w_bits = 0, this.w_mask = 0, this.window = null, this.window_size = 0, this.prev = null, this.head = null, this.ins_h = 0, this.hash_size = 0, this.hash_bits = 0, this.hash_mask = 0, this.hash_shift = 0, this.block_start = 0, this.match_length = 0, this.prev_match = 0, this.match_available = 0, this.strstart = 0, this.match_start = 0, this.lookahead = 0, this.prev_length = 0, this.max_chain_length = 0, this.max_lazy_match = 0, this.level = 0, this.strategy = 0, this.good_match = 0, this.nice_match = 0, this.dyn_ltree = new f.Buf16(2 * P), this.dyn_dtree = new f.Buf16(2 * (2 * a + 1)), this.bl_tree = new f.Buf16(2 * (2 * s + 1)), O(this.dyn_ltree), O(this.dyn_dtree), O(this.bl_tree), this.l_desc = null, this.d_desc = null, this.bl_desc = null, this.bl_count = new f.Buf16(T + 1), this.heap = new f.Buf16(2 * i + 1), O(this.heap), this.heap_len = 0, this.heap_max = 0, this.depth = new f.Buf16(2 * i + 1), O(this.depth), this.l_buf = 0, this.lit_bufsize = 0, this.last_lit = 0, this.d_buf = 0, this.opt_len = 0, this.static_len = 0, this.matches = 0, this.insert = 0, this.bi_buf = 0, this.bi_valid = 0
                }

                function H(e) {
                    var t;
                    return e && e.state ? (e.total_in = e.total_out = 0, e.data_type = o, (t = e.state).pending = 0, t.pending_out = 0, t.wrap < 0 && (t.wrap = -t.wrap), t.status = t.wrap ? b : w, e.adler = 2 === t.wrap ? 0 : 1, t.last_flush = l, c._tr_init(t), y) : N(e, v)
                }

                function q(e) {
                    var t = H(e);
                    return t === y && function (e) {
                        e.window_size = 2 * e.w_size, O(e.head), e.max_lazy_match = u[e.level].max_lazy, e.good_match = u[e.level].good_length, e.nice_match = u[e.level].nice_length, e.max_chain_length = u[e.level].max_chain, e.strstart = 0, e.block_start = 0, e.lookahead = 0, e.insert = 0, e.match_length = e.prev_length = C - 1, e.match_available = 0, e.ins_h = 0
                    }(e.state), t
                }

                function Y(e, t, r, n, o, i) {
                    if (!e) return v;
                    var a = 1;
                    if (t === m && (t = 6), n < 0 ? (a = 0, n = -n) : 15 < n && (a = 2, n -= 16), o < 1 || _ < o || r !== D || n < 8 || 15 < n || t < 0 || 9 < t || i < 0 || h < i) return N(e, v);
                    8 === n && (n = 9);
                    var s = new G;
                    return (e.state = s).strm = e, s.wrap = a, s.gzhead = null, s.w_bits = n, s.w_size = 1 << s.w_bits, s.w_mask = s.w_size - 1, s.hash_bits = o + 7, s.hash_size = 1 << s.hash_bits, s.hash_mask = s.hash_size - 1, s.hash_shift = ~~((s.hash_bits + C - 1) / C), s.window = new f.Buf8(2 * s.w_size), s.head = new f.Buf16(s.hash_size), s.prev = new f.Buf16(s.w_size), s.lit_bufsize = 1 << o + 6, s.pending_buf_size = 4 * s.lit_bufsize, s.pending_buf = new f.Buf8(s.pending_buf_size), s.d_buf = 1 * s.lit_bufsize, s.l_buf = 3 * s.lit_bufsize, s.level = t, s.strategy = i, s.method = r, q(e)
                }

                u = [new F(0, 0, 0, 0, function (e, t) {
                    var r = 65535;
                    for (r > e.pending_buf_size - 5 && (r = e.pending_buf_size - 5); ;) {
                        if (e.lookahead <= 1) {
                            if (K(e), 0 === e.lookahead && t === l) return k;
                            if (0 === e.lookahead) break
                        }
                        e.strstart += e.lookahead, e.lookahead = 0;
                        var n = e.block_start + r;
                        if ((0 === e.strstart || e.strstart >= n) && (e.lookahead = e.strstart - n, e.strstart = n, R(e, !1), 0 === e.strm.avail_out)) return k;
                        if (e.strstart - e.block_start >= e.w_size - I && (R(e, !1), 0 === e.strm.avail_out)) return k
                    }
                    return e.insert = 0, t === d ? (R(e, !0), 0 === e.strm.avail_out ? A : U) : (e.strstart > e.block_start && (R(e, !1), e.strm.avail_out), k)
                }), new F(4, 4, 8, 4, M), new F(4, 5, 16, 8, M), new F(4, 6, 32, 32, M), new F(4, 4, 16, 16, j), new F(8, 16, 32, 32, j), new F(8, 16, 128, 128, j), new F(8, 32, 128, 256, j), new F(32, 128, 258, 1024, j), new F(32, 258, 258, 4096, j)], r.deflateInit = function (e, t) {
                    return Y(e, t, D, 15, 8, 0)
                }, r.deflateInit2 = Y, r.deflateReset = q, r.deflateResetKeep = H, r.deflateSetHeader = function (e, t) {
                    return e && e.state ? 2 !== e.state.wrap ? v : (e.state.gzhead = t, y) : v
                }, r.deflate = function (e, t) {
                    var r, n, o, i;
                    if (!e || !e.state || 5 < t || t < 0) return e ? N(e, v) : v;
                    if (n = e.state, !e.output || !e.input && 0 !== e.avail_in || 666 === n.status && t !== d) return N(e, 0 === e.avail_out ? -5 : v);
                    if (n.strm = e, r = n.last_flush, n.last_flush = t, n.status === b) if (2 === n.wrap) e.adler = 0, V(n, 31), V(n, 139), V(n, 8), n.gzhead ? (V(n, (n.gzhead.text ? 1 : 0) + (n.gzhead.hcrc ? 2 : 0) + (n.gzhead.extra ? 4 : 0) + (n.gzhead.name ? 8 : 0) + (n.gzhead.comment ? 16 : 0)), V(n, 255 & n.gzhead.time), V(n, n.gzhead.time >> 8 & 255), V(n, n.gzhead.time >> 16 & 255), V(n, n.gzhead.time >> 24 & 255), V(n, 9 === n.level ? 2 : 2 <= n.strategy || n.level < 2 ? 4 : 0), V(n, 255 & n.gzhead.os), n.gzhead.extra && n.gzhead.extra.length && (V(n, 255 & n.gzhead.extra.length), V(n, n.gzhead.extra.length >> 8 & 255)), n.gzhead.hcrc && (e.adler = g(e.adler, n.pending_buf, n.pending, 0)), n.gzindex = 0, n.status = 69) : (V(n, 0), V(n, 0), V(n, 0), V(n, 0), V(n, 0), V(n, 9 === n.level ? 2 : 2 <= n.strategy || n.level < 2 ? 4 : 0), V(n, 3), n.status = w); else {
                        var a = D + (n.w_bits - 8 << 4) << 8;
                        a |= (2 <= n.strategy || n.level < 2 ? 0 : n.level < 6 ? 1 : 6 === n.level ? 2 : 3) << 6, 0 !== n.strstart && (a |= 32), a += 31 - a % 31, n.status = w, B(n, a), 0 !== n.strstart && (B(n, e.adler >>> 16), B(n, 65535 & e.adler)), e.adler = 1
                    }
                    if (69 === n.status) if (n.gzhead.extra) {
                        for (o = n.pending; n.gzindex < (65535 & n.gzhead.extra.length) && (n.pending !== n.pending_buf_size || (n.gzhead.hcrc && n.pending > o && (e.adler = g(e.adler, n.pending_buf, n.pending - o, o)), L(e), o = n.pending, n.pending !== n.pending_buf_size));) V(n, 255 & n.gzhead.extra[n.gzindex]), n.gzindex++;
                        n.gzhead.hcrc && n.pending > o && (e.adler = g(e.adler, n.pending_buf, n.pending - o, o)), n.gzindex === n.gzhead.extra.length && (n.gzindex = 0, n.status = 73)
                    } else n.status = 73;
                    if (73 === n.status) if (n.gzhead.name) {
                        o = n.pending;
                        do {
                            if (n.pending === n.pending_buf_size && (n.gzhead.hcrc && n.pending > o && (e.adler = g(e.adler, n.pending_buf, n.pending - o, o)), L(e), o = n.pending, n.pending === n.pending_buf_size)) {
                                i = 1;
                                break
                            }
                            i = n.gzindex < n.gzhead.name.length ? 255 & n.gzhead.name.charCodeAt(n.gzindex++) : 0, V(n, i)
                        } while (0 !== i);
                        n.gzhead.hcrc && n.pending > o && (e.adler = g(e.adler, n.pending_buf, n.pending - o, o)), 0 === i && (n.gzindex = 0, n.status = 91)
                    } else n.status = 91;
                    if (91 === n.status) if (n.gzhead.comment) {
                        o = n.pending;
                        do {
                            if (n.pending === n.pending_buf_size && (n.gzhead.hcrc && n.pending > o && (e.adler = g(e.adler, n.pending_buf, n.pending - o, o)), L(e), o = n.pending, n.pending === n.pending_buf_size)) {
                                i = 1;
                                break
                            }
                            i = n.gzindex < n.gzhead.comment.length ? 255 & n.gzhead.comment.charCodeAt(n.gzindex++) : 0, V(n, i)
                        } while (0 !== i);
                        n.gzhead.hcrc && n.pending > o && (e.adler = g(e.adler, n.pending_buf, n.pending - o, o)), 0 === i && (n.status = 103)
                    } else n.status = 103;
                    if (103 === n.status && (n.gzhead.hcrc ? (n.pending + 2 > n.pending_buf_size && L(e), n.pending + 2 <= n.pending_buf_size && (V(n, 255 & e.adler), V(n, e.adler >> 8 & 255), e.adler = 0, n.status = w)) : n.status = w), 0 !== n.pending) {
                        if (L(e), 0 === e.avail_out) return n.last_flush = -1, y
                    } else if (0 === e.avail_in && x(t) <= x(r) && t !== d) return N(e, -5);
                    if (666 === n.status && 0 !== e.avail_in) return N(e, -5);
                    if (0 !== e.avail_in || 0 !== n.lookahead || t !== l && 666 !== n.status) {
                        var s = 2 === n.strategy ? function (e, t) {
                            for (var r; ;) {
                                if (0 === e.lookahead && (K(e), 0 === e.lookahead)) {
                                    if (t === l) return k;
                                    break
                                }
                                if (e.match_length = 0, r = c._tr_tally(e, 0, e.window[e.strstart]), e.lookahead--, e.strstart++, r && (R(e, !1), 0 === e.strm.avail_out)) return k
                            }
                            return e.insert = 0, t === d ? (R(e, !0), 0 === e.strm.avail_out ? A : U) : e.last_lit && (R(e, !1), 0 === e.strm.avail_out) ? k : E
                        }(n, t) : 3 === n.strategy ? function (e, t) {
                            for (var r, n, o, i, a = e.window; ;) {
                                if (e.lookahead <= S) {
                                    if (K(e), e.lookahead <= S && t === l) return k;
                                    if (0 === e.lookahead) break
                                }
                                if (e.match_length = 0, e.lookahead >= C && 0 < e.strstart && (n = a[o = e.strstart - 1]) === a[++o] && n === a[++o] && n === a[++o]) {
                                    i = e.strstart + S;
                                    do {
                                    } while (n === a[++o] && n === a[++o] && n === a[++o] && n === a[++o] && n === a[++o] && n === a[++o] && n === a[++o] && n === a[++o] && o < i);
                                    e.match_length = S - (i - o), e.match_length > e.lookahead && (e.match_length = e.lookahead)
                                }
                                if (e.match_length >= C ? (r = c._tr_tally(e, 1, e.match_length - C), e.lookahead -= e.match_length, e.strstart += e.match_length, e.match_length = 0) : (r = c._tr_tally(e, 0, e.window[e.strstart]), e.lookahead--, e.strstart++), r && (R(e, !1), 0 === e.strm.avail_out)) return k
                            }
                            return e.insert = 0, t === d ? (R(e, !0), 0 === e.strm.avail_out ? A : U) : e.last_lit && (R(e, !1), 0 === e.strm.avail_out) ? k : E
                        }(n, t) : u[n.level].func(n, t);
                        if (s !== A && s !== U || (n.status = 666), s === k || s === A) return 0 === e.avail_out && (n.last_flush = -1), y;
                        if (s === E && (1 === t ? c._tr_align(n) : 5 !== t && (c._tr_stored_block(n, 0, 0, !1), 3 === t && (O(n.head), 0 === n.lookahead && (n.strstart = 0, n.block_start = 0, n.insert = 0))), L(e), 0 === e.avail_out)) return n.last_flush = -1, y
                    }
                    return t !== d ? y : n.wrap <= 0 ? 1 : (2 === n.wrap ? (V(n, 255 & e.adler), V(n, e.adler >> 8 & 255), V(n, e.adler >> 16 & 255), V(n, e.adler >> 24 & 255), V(n, 255 & e.total_in), V(n, e.total_in >> 8 & 255), V(n, e.total_in >> 16 & 255), V(n, e.total_in >> 24 & 255)) : (B(n, e.adler >>> 16), B(n, 65535 & e.adler)), L(e), 0 < n.wrap && (n.wrap = -n.wrap), 0 !== n.pending ? y : 1)
                }, r.deflateEnd = function (e) {
                    var t;
                    return e && e.state ? (t = e.state.status) !== b && 69 !== t && 73 !== t && 91 !== t && 103 !== t && t !== w && 666 !== t ? N(e, v) : (e.state = null, t === w ? N(e, -3) : y) : v
                }, r.deflateSetDictionary = function (e, t) {
                    var r, n, o, i, a, s, u, c, l = t.length;
                    if (!e || !e.state) return v;
                    if (2 === (i = (r = e.state).wrap) || 1 === i && r.status !== b || r.lookahead) return v;
                    for (1 === i && (e.adler = p(e.adler, t, l, 0)), r.wrap = 0, l >= r.w_size && (0 === i && (O(r.head), r.strstart = 0, r.block_start = 0, r.insert = 0), c = new f.Buf8(r.w_size), f.arraySet(c, t, l - r.w_size, r.w_size, 0), t = c, l = r.w_size), a = e.avail_in, s = e.next_in, u = e.input, e.avail_in = l, e.next_in = 0, e.input = t, K(r); r.lookahead >= C;) {
                        for (n = r.strstart, o = r.lookahead - (C - 1); r.ins_h = (r.ins_h << r.hash_shift ^ r.window[n + C - 1]) & r.hash_mask, r.prev[n & r.w_mask] = r.head[r.ins_h], r.head[r.ins_h] = n, n++, --o;) ;
                        r.strstart = n, r.lookahead = C - 1, K(r)
                    }
                    return r.strstart += r.lookahead, r.block_start = r.strstart, r.insert = r.lookahead, r.lookahead = 0, r.match_length = r.prev_length = C - 1, r.match_available = 0, e.next_in = s, e.input = u, e.avail_in = a, r.wrap = i, y
                }, r.deflateInfo = "pako deflate (from Nodeca project)"
            }, {"../utils/common": 1, "./adler32": 3, "./crc32": 4, "./messages": 6, "./trees": 7}],
            6: [function (e, t, r) {
                "use strict";
                t.exports = {
                    2: "need dictionary",
                    1: "stream end",
                    0: "",
                    "-1": "file error",
                    "-2": "stream error",
                    "-3": "data error",
                    "-4": "insufficient memory",
                    "-5": "buffer error",
                    "-6": "incompatible version"
                }
            }, {}],
            7: [function (e, t, r) {
                "use strict";
                var o = e("../utils/common"), s = 0, u = 1;

                function n(e) {
                    for (var t = e.length; 0 <= --t;) e[t] = 0
                }

                var i = 0, a = 29, c = 256, l = c + 1 + a, d = 30, f = 19, v = 2 * l + 1, m = 15, p = 16, g = 7,
                    y = 256, h = 16, D = 17, _ = 18,
                    P = [0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0],
                    T = [0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13],
                    C = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 7],
                    S = [16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15], I = new Array(2 * (l + 2));
                n(I);
                var b = new Array(2 * d);
                n(b);
                var w = new Array(512);
                n(w);
                var k = new Array(256);
                n(k);
                var E = new Array(a);
                n(E);
                var A, U, N, x = new Array(d);

                function O(e, t, r, n, o) {
                    this.static_tree = e, this.extra_bits = t, this.extra_base = r, this.elems = n, this.max_length = o, this.has_stree = e && e.length
                }

                function L(e, t) {
                    this.dyn_tree = e, this.max_code = 0, this.stat_desc = t
                }

                function R(e) {
                    return e < 256 ? w[e] : w[256 + (e >>> 7)]
                }

                function V(e, t) {
                    e.pending_buf[e.pending++] = 255 & t, e.pending_buf[e.pending++] = t >>> 8 & 255
                }

                function B(e, t, r) {
                    e.bi_valid > p - r ? (e.bi_buf |= t << e.bi_valid & 65535, V(e, e.bi_buf), e.bi_buf = t >> p - e.bi_valid, e.bi_valid += r - p) : (e.bi_buf |= t << e.bi_valid & 65535, e.bi_valid += r)
                }

                function z(e, t, r) {
                    B(e, r[2 * t], r[2 * t + 1])
                }

                function K(e, t) {
                    for (var r = 0; r |= 1 & e, e >>>= 1, r <<= 1, 0 < --t;) ;
                    return r >>> 1
                }

                function M(e, t, r) {
                    var n, o, i = new Array(m + 1), a = 0;
                    for (n = 1; n <= m; n++) i[n] = a = a + r[n - 1] << 1;
                    for (o = 0; o <= t; o++) {
                        var s = e[2 * o + 1];
                        0 !== s && (e[2 * o] = K(i[s]++, s))
                    }
                }

                function j(e) {
                    var t;
                    for (t = 0; t < l; t++) e.dyn_ltree[2 * t] = 0;
                    for (t = 0; t < d; t++) e.dyn_dtree[2 * t] = 0;
                    for (t = 0; t < f; t++) e.bl_tree[2 * t] = 0;
                    e.dyn_ltree[2 * y] = 1, e.opt_len = e.static_len = 0, e.last_lit = e.matches = 0
                }

                function F(e) {
                    8 < e.bi_valid ? V(e, e.bi_buf) : 0 < e.bi_valid && (e.pending_buf[e.pending++] = e.bi_buf), e.bi_buf = 0, e.bi_valid = 0
                }

                function G(e, t, r, n) {
                    var o = 2 * t, i = 2 * r;
                    return e[o] < e[i] || e[o] === e[i] && n[t] <= n[r]
                }

                function H(e, t, r) {
                    for (var n = e.heap[r], o = r << 1; o <= e.heap_len && (o < e.heap_len && G(t, e.heap[o + 1], e.heap[o], e.depth) && o++, !G(t, n, e.heap[o], e.depth));) e.heap[r] = e.heap[o], r = o, o <<= 1;
                    e.heap[r] = n
                }

                function q(e, t, r) {
                    var n, o, i, a, s = 0;
                    if (0 !== e.last_lit) for (; n = e.pending_buf[e.d_buf + 2 * s] << 8 | e.pending_buf[e.d_buf + 2 * s + 1], o = e.pending_buf[e.l_buf + s], s++, 0 === n ? z(e, o, t) : (z(e, (i = k[o]) + c + 1, t), 0 !== (a = P[i]) && B(e, o -= E[i], a), z(e, i = R(--n), r), 0 !== (a = T[i]) && B(e, n -= x[i], a)), s < e.last_lit;) ;
                    z(e, y, t)
                }

                function Y(e, t) {
                    var r, n, o, i = t.dyn_tree, a = t.stat_desc.static_tree, s = t.stat_desc.has_stree,
                        u = t.stat_desc.elems, c = -1;
                    for (e.heap_len = 0, e.heap_max = v, r = 0; r < u; r++) 0 !== i[2 * r] ? (e.heap[++e.heap_len] = c = r, e.depth[r] = 0) : i[2 * r + 1] = 0;
                    for (; e.heap_len < 2;) i[2 * (o = e.heap[++e.heap_len] = c < 2 ? ++c : 0)] = 1, e.depth[o] = 0, e.opt_len--, s && (e.static_len -= a[2 * o + 1]);
                    for (t.max_code = c, r = e.heap_len >> 1; 1 <= r; r--) H(e, i, r);
                    for (o = u; r = e.heap[1], e.heap[1] = e.heap[e.heap_len--], H(e, i, 1), n = e.heap[1], e.heap[--e.heap_max] = r, e.heap[--e.heap_max] = n, i[2 * o] = i[2 * r] + i[2 * n], e.depth[o] = (e.depth[r] >= e.depth[n] ? e.depth[r] : e.depth[n]) + 1, i[2 * r + 1] = i[2 * n + 1] = o, e.heap[1] = o++, H(e, i, 1), 2 <= e.heap_len;) ;
                    e.heap[--e.heap_max] = e.heap[1], function (e, t) {
                        var r, n, o, i, a, s, u = t.dyn_tree, c = t.max_code, l = t.stat_desc.static_tree,
                            d = t.stat_desc.has_stree, f = t.stat_desc.extra_bits, p = t.stat_desc.extra_base,
                            g = t.stat_desc.max_length, y = 0;
                        for (i = 0; i <= m; i++) e.bl_count[i] = 0;
                        for (u[2 * e.heap[e.heap_max] + 1] = 0, r = e.heap_max + 1; r < v; r++) g < (i = u[2 * u[2 * (n = e.heap[r]) + 1] + 1] + 1) && (i = g, y++), u[2 * n + 1] = i, c < n || (e.bl_count[i]++, a = 0, p <= n && (a = f[n - p]), s = u[2 * n], e.opt_len += s * (i + a), d && (e.static_len += s * (l[2 * n + 1] + a)));
                        if (0 !== y) {
                            do {
                                for (i = g - 1; 0 === e.bl_count[i];) i--;
                                e.bl_count[i]--, e.bl_count[i + 1] += 2, e.bl_count[g]--, y -= 2
                            } while (0 < y);
                            for (i = g; 0 !== i; i--) for (n = e.bl_count[i]; 0 !== n;) c < (o = e.heap[--r]) || (u[2 * o + 1] !== i && (e.opt_len += (i - u[2 * o + 1]) * u[2 * o], u[2 * o + 1] = i), n--)
                        }
                    }(e, t), M(i, c, e.bl_count)
                }

                function W(e, t, r) {
                    var n, o, i = -1, a = t[1], s = 0, u = 7, c = 4;
                    for (0 === a && (u = 138, c = 3), t[2 * (r + 1) + 1] = 65535, n = 0; n <= r; n++) o = a, a = t[2 * (n + 1) + 1], ++s < u && o === a || (s < c ? e.bl_tree[2 * o] += s : 0 !== o ? (o !== i && e.bl_tree[2 * o]++, e.bl_tree[2 * h]++) : s <= 10 ? e.bl_tree[2 * D]++ : e.bl_tree[2 * _]++, i = o, c = (s = 0) === a ? (u = 138, 3) : o === a ? (u = 6, 3) : (u = 7, 4))
                }

                function J(e, t, r) {
                    var n, o, i = -1, a = t[1], s = 0, u = 7, c = 4;
                    for (0 === a && (u = 138, c = 3), n = 0; n <= r; n++) if (o = a, a = t[2 * (n + 1) + 1], !(++s < u && o === a)) {
                        if (s < c) for (; z(e, o, e.bl_tree), 0 != --s;) ; else 0 !== o ? (o !== i && (z(e, o, e.bl_tree), s--), z(e, h, e.bl_tree), B(e, s - 3, 2)) : s <= 10 ? (z(e, D, e.bl_tree), B(e, s - 3, 3)) : (z(e, _, e.bl_tree), B(e, s - 11, 7));
                        i = o, c = (s = 0) === a ? (u = 138, 3) : o === a ? (u = 6, 3) : (u = 7, 4)
                    }
                }

                n(x);
                var X = !1;

                function Z(e, t, r, n) {
                    B(e, (i << 1) + (n ? 1 : 0), 3), function (e, t, r, n) {
                        F(e), n && (V(e, r), V(e, ~r)), o.arraySet(e.pending_buf, e.window, t, r, e.pending), e.pending += r
                    }(e, t, r, !0)
                }

                r._tr_init = function (e) {
                    X || (function () {
                        var e, t, r, n, o, i = new Array(m + 1);
                        for (n = r = 0; n < a - 1; n++) for (E[n] = r, e = 0; e < 1 << P[n]; e++) k[r++] = n;
                        for (k[r - 1] = n, n = o = 0; n < 16; n++) for (x[n] = o, e = 0; e < 1 << T[n]; e++) w[o++] = n;
                        for (o >>= 7; n < d; n++) for (x[n] = o << 7, e = 0; e < 1 << T[n] - 7; e++) w[256 + o++] = n;
                        for (t = 0; t <= m; t++) i[t] = 0;
                        for (e = 0; e <= 143;) I[2 * e + 1] = 8, e++, i[8]++;
                        for (; e <= 255;) I[2 * e + 1] = 9, e++, i[9]++;
                        for (; e <= 279;) I[2 * e + 1] = 7, e++, i[7]++;
                        for (; e <= 287;) I[2 * e + 1] = 8, e++, i[8]++;
                        for (M(I, l + 1, i), e = 0; e < d; e++) b[2 * e + 1] = 5, b[2 * e] = K(e, 5);
                        A = new O(I, P, c + 1, l, m), U = new O(b, T, 0, d, m), N = new O(new Array(0), C, 0, f, g)
                    }(), X = !0), e.l_desc = new L(e.dyn_ltree, A), e.d_desc = new L(e.dyn_dtree, U), e.bl_desc = new L(e.bl_tree, N), e.bi_buf = 0, e.bi_valid = 0, j(e)
                }, r._tr_stored_block = Z, r._tr_flush_block = function (e, t, r, n) {
                    var o, i, a = 0;
                    0 < e.level ? (2 === e.strm.data_type && (e.strm.data_type = function (e) {
                        var t, r = 4093624447;
                        for (t = 0; t <= 31; t++, r >>>= 1) if (1 & r && 0 !== e.dyn_ltree[2 * t]) return s;
                        if (0 !== e.dyn_ltree[18] || 0 !== e.dyn_ltree[20] || 0 !== e.dyn_ltree[26]) return u;
                        for (t = 32; t < c; t++) if (0 !== e.dyn_ltree[2 * t]) return u;
                        return s
                    }(e)), Y(e, e.l_desc), Y(e, e.d_desc), a = function (e) {
                        var t;
                        for (W(e, e.dyn_ltree, e.l_desc.max_code), W(e, e.dyn_dtree, e.d_desc.max_code), Y(e, e.bl_desc), t = f - 1; 3 <= t && 0 === e.bl_tree[2 * S[t] + 1]; t--) ;
                        return e.opt_len += 3 * (t + 1) + 5 + 5 + 4, t
                    }(e), o = e.opt_len + 3 + 7 >>> 3, (i = e.static_len + 3 + 7 >>> 3) <= o && (o = i)) : o = i = r + 5, r + 4 <= o && -1 !== t ? Z(e, t, r, n) : 4 === e.strategy || i === o ? (B(e, 2 + (n ? 1 : 0), 3), q(e, I, b)) : (B(e, 4 + (n ? 1 : 0), 3), function (e, t, r, n) {
                        var o;
                        for (B(e, t - 257, 5), B(e, r - 1, 5), B(e, n - 4, 4), o = 0; o < n; o++) B(e, e.bl_tree[2 * S[o] + 1], 3);
                        J(e, e.dyn_ltree, t - 1), J(e, e.dyn_dtree, r - 1)
                    }(e, e.l_desc.max_code + 1, e.d_desc.max_code + 1, a + 1), q(e, e.dyn_ltree, e.dyn_dtree)), j(e), n && F(e)
                }, r._tr_tally = function (e, t, r) {
                    return e.pending_buf[e.d_buf + 2 * e.last_lit] = t >>> 8 & 255, e.pending_buf[e.d_buf + 2 * e.last_lit + 1] = 255 & t, e.pending_buf[e.l_buf + e.last_lit] = 255 & r, e.last_lit++, 0 === t ? e.dyn_ltree[2 * r]++ : (e.matches++, t--, e.dyn_ltree[2 * (k[r] + c + 1)]++, e.dyn_dtree[2 * R(t)]++), e.last_lit === e.lit_bufsize - 1
                }, r._tr_align = function (e) {
                    B(e, 2, 3), z(e, y, I), function (e) {
                        16 === e.bi_valid ? (V(e, e.bi_buf), e.bi_buf = 0, e.bi_valid = 0) : 8 <= e.bi_valid && (e.pending_buf[e.pending++] = 255 & e.bi_buf, e.bi_buf >>= 8, e.bi_valid -= 8)
                    }(e)
                }
            }, {"../utils/common": 1}],
            8: [function (e, t, r) {
                "use strict";
                t.exports = function () {
                    this.input = null, this.next_in = 0, this.avail_in = 0, this.total_in = 0, this.output = null, this.next_out = 0, this.avail_out = 0, this.total_out = 0, this.msg = "", this.state = null, this.data_type = 2, this.adler = 0
                }
            }, {}],
            "/lib/deflate.js": [function (e, t, r) {
                "use strict";
                var a = e("./zlib/deflate"), s = e("./utils/common"), u = e("./utils/strings"),
                    o = e("./zlib/messages"), i = e("./zlib/zstream"), c = Object.prototype.toString, l = 0, d = -1,
                    f = 0, p = 8;

                function g(e) {
                    if (!(this instanceof g)) return new g(e);
                    this.options = s.assign({
                        level: d,
                        method: p,
                        chunkSize: 16384,
                        windowBits: 15,
                        memLevel: 8,
                        strategy: f,
                        to: ""
                    }, e || {});
                    var t = this.options;
                    t.raw && 0 < t.windowBits ? t.windowBits = -t.windowBits : t.gzip && 0 < t.windowBits && t.windowBits < 16 && (t.windowBits += 16), this.err = 0, this.msg = "", this.ended = !1, this.chunks = [], this.strm = new i, this.strm.avail_out = 0;
                    var r = a.deflateInit2(this.strm, t.level, t.method, t.windowBits, t.memLevel, t.strategy);
                    if (r !== l) throw new Error(o[r]);
                    if (t.header && a.deflateSetHeader(this.strm, t.header), t.dictionary) {
                        var n;
                        if (n = "string" == typeof t.dictionary ? u.string2buf(t.dictionary) : "[object ArrayBuffer]" === c.call(t.dictionary) ? new Uint8Array(t.dictionary) : t.dictionary, (r = a.deflateSetDictionary(this.strm, n)) !== l) throw new Error(o[r]);
                        this._dict_set = !0
                    }
                }

                function n(e, t) {
                    var r = new g(t);
                    if (r.push(e, !0), r.err) throw r.msg || o[r.err];
                    return r.result
                }

                g.prototype.push = function (e, t) {
                    var r, n, o = this.strm, i = this.options.chunkSize;
                    if (this.ended) return !1;
                    n = t === ~~t ? t : !0 === t ? 4 : 0, "string" == typeof e ? o.input = u.string2buf(e) : "[object ArrayBuffer]" === c.call(e) ? o.input = new Uint8Array(e) : o.input = e, o.next_in = 0, o.avail_in = o.input.length;
                    do {
                        if (0 === o.avail_out && (o.output = new s.Buf8(i), o.next_out = 0, o.avail_out = i), 1 !== (r = a.deflate(o, n)) && r !== l) return this.onEnd(r), !(this.ended = !0);
                        0 !== o.avail_out && (0 !== o.avail_in || 4 !== n && 2 !== n) || ("string" === this.options.to ? this.onData(u.buf2binstring(s.shrinkBuf(o.output, o.next_out))) : this.onData(s.shrinkBuf(o.output, o.next_out)))
                    } while ((0 < o.avail_in || 0 === o.avail_out) && 1 !== r);
                    return 4 === n ? (r = a.deflateEnd(this.strm), this.onEnd(r), this.ended = !0, r === l) : 2 !== n || (this.onEnd(l), !(o.avail_out = 0))
                }, g.prototype.onData = function (e) {
                    this.chunks.push(e)
                }, g.prototype.onEnd = function (e) {
                    e === l && ("string" === this.options.to ? this.result = this.chunks.join("") : this.result = s.flattenChunks(this.chunks)), this.chunks = [], this.err = e, this.msg = this.strm.msg
                }, r.Deflate = g, r.deflate = n, r.deflateRaw = function (e, t) {
                    return (t = t || {}).raw = !0, n(e, t)
                }, r.gzip = function (e, t) {
                    return (t = t || {}).gzip = !0, n(e, t)
                }
            }, {
                "./utils/common": 1,
                "./utils/strings": 2,
                "./zlib/deflate": 5,
                "./zlib/messages": 6,
                "./zlib/zstream": 8
            }]
        }, {}, [])("/lib/deflate.js")
    });
    var Base64 = {
            _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) {
                var t, r, n, o, i, a, s, u = "", c = 0;
                for (e = Base64._utf8_encode(e); c < e.length;) o = (t = e.charCodeAt(c++)) >> 2, i = (3 & t) << 4 | (r = e.charCodeAt(c++)) >> 4, a = (15 & r) << 2 | (n = e.charCodeAt(c++)) >> 6, s = 63 & n, isNaN(r) ? a = s = 64 : isNaN(n) && (s = 64), u = u + this._keyStr.charAt(o) + this._keyStr.charAt(i) + this._keyStr.charAt(a) + this._keyStr.charAt(s);
                return u
            }, decode: function (e) {
                var t, r, n, o, i, a, s = "", u = 0;
                for (e = e.replace(/[^A-Za-z0-9\+\/\=]/g, ""); u < e.length;) t = this._keyStr.indexOf(e.charAt(u++)) << 2 | (o = this._keyStr.indexOf(e.charAt(u++))) >> 4, r = (15 & o) << 4 | (i = this._keyStr.indexOf(e.charAt(u++))) >> 2, n = (3 & i) << 6 | (a = this._keyStr.indexOf(e.charAt(u++))), s += String.fromCharCode(t), 64 != i && (s += String.fromCharCode(r)), 64 != a && (s += String.fromCharCode(n));
                return s = Base64._utf8_decode(s)
            }, _utf8_encode: function (e) {
                e = e.replace(/\r\n/g, "\n");
                for (var t = "", r = 0; r < e.length; r++) {
                    var n = e.charCodeAt(r);
                    n < 128 ? t += String.fromCharCode(n) : (127 < n && n < 2048 ? t += String.fromCharCode(n >> 6 | 192) : (t += String.fromCharCode(n >> 12 | 224), t += String.fromCharCode(n >> 6 & 63 | 128)), t += String.fromCharCode(63 & n | 128))
                }
                return t
            }, _utf8_decode: function (e) {
                for (var t = "", r = 0, n = c1 = c2 = 0; r < e.length;) (n = e.charCodeAt(r)) < 128 ? (t += String.fromCharCode(n), r++) : 191 < n && n < 224 ? (c2 = e.charCodeAt(r + 1), t += String.fromCharCode((31 & n) << 6 | 63 & c2), r += 2) : (c2 = e.charCodeAt(r + 1), c3 = e.charCodeAt(r + 2), t += String.fromCharCode((15 & n) << 12 | (63 & c2) << 6 | 63 & c3), r += 3);
                return t
            }
        }, UNKNOWN = "?", NAME = "name", VERSION = "version", util = {
            has: function (e, t) {
                return "string" == typeof e && -1 !== t.toLowerCase().indexOf(e.toLowerCase())
            }
        }, mapper = {
            rgx: function (e, t) {
                for (var r, n, o, i, a, s, u = 0; u < t.length && !a;) {
                    var c = t[u], l = t[u + 1];
                    for (r = n = 0; r < c.length && !a;) if (a = c[r++].exec(e)) for (o = 0; o < l.length; o++) s = a[++n], "object" === _typeof(i = l[o]) && 0 < i.length ? 2 == i.length ? "function" == typeof i[1] ? this[i[0]] = i[1].call(this, s) : this[i[0]] = i[1] : 3 == i.length ? "function" != typeof i[1] || i[1].exec && i[1].test ? this[i[0]] = s ? s.replace(i[1], i[2]) : void 0 : this[i[0]] = s ? i[1].call(this, s, i[2]) : void 0 : 4 == i.length && (this[i[0]] = s ? i[3].call(this, s.replace(i[1], i[2])) : void 0) : this[i] = s || void 0;
                    u += 2
                }
            }, str: function (e, t) {
                for (var r in t) if ("object" === _typeof(t[r]) && 0 < t[r].length) {
                    for (var n = 0; n < t[r].length; n++) if (util.has(t[r][n], e)) return r === UNKNOWN ? void 0 : r
                } else if (util.has(t[r], e)) return r === UNKNOWN ? void 0 : r;
                return e
            }
        }, maps = {
            os: {
                windows: {
                    version: {
                        ME: "4.90",
                        "NT 3.11": "NT3.51",
                        "NT 4.0": "NT4.0",
                        2e3: "NT 5.0",
                        XP: ["NT 5.1", "NT 5.2"],
                        Vista: "NT 6.0",
                        7: "NT 6.1",
                        8: "NT 6.2",
                        8.1: "NT 6.3",
                        10: ["NT 6.4", "NT 10.0"],
                        RT: "ARM"
                    }
                }
            }
        }, regexes = {
            os: [[/microsoft\s(windows)\s(vista|xp)/i], [NAME, VERSION], [/(windows)\snt\s6\.2;\s(arm)/i, /(windows\sphone(?:\sos)*)[\s\/]?([\d\.\s\w]*)/i, /(windows\smobile|windows)[\s\/]?([ntce\d\.\s]+\w)/i], [NAME, [VERSION, mapper.str, maps.os.windows.version]], [/(win(?=3|9|n)|win\s9x\s)([nt\d\.]+)/i], [[NAME, "Windows"], [VERSION, mapper.str, maps.os.windows.version]], [/\((bb)(10);/i], [[NAME, "BlackBerry"], VERSION], [/(blackberry)\w*\/?([\w\.]*)/i, /(tizen|kaios)[\/\s]([\w\.]+)/i, /(android|webos|palm\sos|qnx|bada|rim\stablet\sos|meego|sailfish|contiki)[\/\s-]?([\w\.]*)/i], [NAME, VERSION], [/(symbian\s?os|symbos|s60(?=;))[\/\s-]?([\w\.]*)/i], [[NAME, "Symbian"], VERSION], [/\((series40);/i], [NAME], [/mozilla.+\(mobile;.+gecko.+firefox/i], [[NAME, "Firefox OS"], VERSION], [/(nintendo|playstation)\s([wids34portablevu]+)/i, /(mint)[\/\s\(]?(\w*)/i, /(mageia|vectorlinux)[;\s]/i, /(joli|[kxln]?ubuntu|debian|suse|opensuse|gentoo|(?=\s)arch|slackware|fedora|mandriva|centos|pclinuxos|redhat|zenwalk|linpus)[\/\s-]?(?!chrom)([\w\.-]*)/i, /(hurd|linux)\s?([\w\.]*)/i, /(gnu)\s?([\w\.]*)/i], [NAME, VERSION], [/(cros)\s[\w]+\s([\w\.]+\w)/i], [[NAME, "Chromium OS"], VERSION], [/(sunos)\s?([\w\.\d]*)/i], [[NAME, "Solaris"], VERSION], [/\s([frentopc-]{0,4}bsd|dragonfly)\s?([\w\.]*)/i], [NAME, VERSION], [/(haiku)\s(\w+)/i], [NAME, VERSION], [/cfnetwork\/.+darwin/i, /ip[honead]{2,4}(?:.*os\s([\w]+)\slike\smac|;\sopera)/i], [[VERSION, /_/g, "."], [NAME, "iOS"]], [/(mac\sos\sx)\s?([\w\s\.]*)/i, /(macintosh|mac(?=_powerpc)\s)/i], [[NAME, "Mac OS"], [VERSION, /_/g, "."]], [/((?:open)?solaris)[\/\s-]?([\w\.]*)/i, /(aix)\s((\d)(?=\.|\)|\s)[\w\.])*/i, /(plan\s9|minix|beos|os\/2|amigaos|morphos|risc\sos|openvms|fuchsia)/i, /(unix)\s?([\w\.]*)/i], [NAME, VERSION]],
            bot: ["Slurp", "nuhk", "YandexBot", "YandexAccessibilityBot", "YandexMobileBot", "YandexDirectDyn", "yammybot", "Openbot", "MSNBot", "YandexScreenshotBot", "YandexImages", "YandexVideo", "YandexVideoParser", "YandexMedia", "YandexBlogs", "YandexFavicons", "YandexWebmaster", "YandexPagechecker", "YandexImageResizer", "YandexAdNet", "YandexDirect", "YaDirectFetcher", "YandexCalendar", "YandexSitelinks", "YandexMetrika", "YandexNews", "YandexNewslinks", "YandexCatalog", "YandexAntivirus", "YandexMarket", "YandexVertis", "YandexForDomain", "YandexSpravBot", "YandexSearchShop", "YandexMedianaBot", "YandexOntoDB", "YandexOntoDBAPI", "Googlebot", "Googlebot-Image", "Mediapartners-Google", "AdsBot-Google", "Mail.RU_Bot", "bingbot", "Accoona", "ia_archiver", "Ask Jeeves", "OmniExplorer_Bot", "W3C_Validator", "WebAlta", "YahooFeedSeeker", "Yahoo!", "Ezooms", "Tourlentabot", "MJ12bot", "AhrefsBot", "SearchBot", "SiteStatus", "Nigma.ru", "Baiduspider", "Statsbot", "SISTRIX", "AcoonBot", "findlinks", "proximic", "OpenindexSpider", "statdom.ru", "Exabot", "Spider", "SeznamBot", "oBot", "C-T bot", "Updownerbot", "Snoopy", "heritrix", "Yeti", "DomainVader", "DCPbot", "PaperLiBot"]
        }, DTDHelpers = {
            forEach: function (e, t) {
                for (var r = 0; r < t.length; ++r) e(t[r], r)
            }, buildUrl: function (e, t) {
                var r, n = "";
                for (r in t) n += "" !== n ? "&" : "", n += r + "=" + encodeURIComponent(t[r]);
                return e + "?" + n
            }, format: function (e, r) {
                return 1 === r.length && null !== r[0] && "object" === _typeof(r[0]) && (r = r[0]), e.replace(/{([^}]*)}/g, function (e, t) {
                    return void 0 !== r[t] ? r[t] : e
                })
            }, isNullOrEmpty: function (e) {
                if (DTDHelpers.isUndefined(e)) return !0;
                if ("function" == typeof e || "[object Date]" === Object.prototype.toString.call(e)) return !1;
                if (DTDHelpers.isNumber(e) || DTDHelpers.isBoolean(e)) return DTDHelpers.isNull(e);
                if (DTDHelpers.isNull(e) || 0 === DTDHelpers.trimmer(e).length) return !0;
                if ("object" !== _typeof(e)) return !1;
                for (var t in e) return !1;
                return !0
            }, isNullOrEmptyString: function (e) {
                return !DTDHelpers.isString(e) || DTDHelpers.isNullOrEmpty(e)
            }, isArray: function (e) {
                return -1 < e.constructor.toString().indexOf("Array")
            }, isObject: function (e) {
                return e === Object(e) && !DTDHelpers.isArray(e)
            }, isString: function (e) {
                return "string" == typeof e
            }, isBoolean: function (e) {
                return "boolean" == typeof e
            }, isUndefined: function (e) {
                return void 0 === e || "undefined" === e
            }, isNumber: function (e) {
                return "number" == typeof e && !DTDHelpers.isNaN(e) && e !== Number.POSITIVE_INFINITY && e !== Number.NEGATIVE_INFINITY
            }, isNaN: function (e) {
                return e != e
            }, isNull: function (e) {
                return null === e || "null" === e
            }, generateUUID: function () {
                var r = (new Date).getTime();
                return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (e) {
                    var t = (r + 16 * Math.random()) % 16 | 0;
                    return r = Math.floor(r / 16), ("x" === e ? t : 3 & t | 8).toString(16)
                })
            }, forEachObj: function (e, t) {
                for (var r in t) t.hasOwnProperty(r) && e(t[r], r);
                return {}
            }, generateKey: function (e, t) {
                var r = !(1 < arguments.length && void 0 !== t) || t, n = e.join("_");
                return r && (n = Base64.encode(n)), n
            }, parseStoreData: function (e, t) {
                return (!(1 < arguments.length && void 0 !== t) || t) && (e = Base64.decode(e)), JSON.parse(e)
            }, prepareStoreData: function (e, t) {
                var r = !(1 < arguments.length && void 0 !== t) || t;
                return e = JSON.stringify(e), r && (e = Base64.encode(e)), e
            }, findObj: function (e, t) {
                for (var r in t) if (t.hasOwnProperty(r) && e(t[r], r)) return t[r];
                return null
            }, getCurrentTimeMs: function () {
                return Date.now()
            }, trimmer: function (e) {
                return e = e && e.toString().trim()
            }, filterObj: function (e, t) {
                var r = {};
                for (var n in t) t.hasOwnProperty(n) && e(t[n], n) && (r[n] = t[n]);
                return r
            }, languageToISO639_1: function (e) {
                var t = null;
                return !DTDHelpers.isNullOrEmpty(e) && DTDHelpers.isString(e) && (t = (t = e.split("-"))[0].toLowerCase()), t
            }, isValueISO4217: function (e) {
                var t = /^[A-Z]{3}$/;
                return !(!e || !t) && t.test(e)
            }, isMoreOrEqualThan: function (e, t) {
                return (1 < arguments.length && void 0 !== t ? t : Number.MAX_VALUE) <= e
            }, isMoreThan: function (e, t) {
                return (1 < arguments.length && void 0 !== t ? t : Number.MAX_VALUE) < e
            }, isLessOrEqualThan: function (e, t) {
                return e <= (1 < arguments.length && void 0 !== t ? t : Number.MIN_VALUE)
            }, isLessThan: function (e, t) {
                return e < (1 < arguments.length && void 0 !== t ? t : Number.MIN_VALUE)
            }, isInteger: function (e) {
                return DTDHelpers.isNumber(e) && e === parseInt(e)
            }, parseUrlParams: function (e, t) {
                e = decodeURIComponent(e), t = t.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var r = new RegExp("[\\?&=]" + t + "=([^&#]*)").exec(e);
                return null === r || r && "string" != typeof r[1] && r[1].length ? "" : decodeURIComponent(r[1]).replace(/\+/g, " ")
            }, mergeRecursive: function (t, r) {
                for (var n in r) try {
                    r[n].constructor === Object ? t[n] = DTDHelpers.mergeRecursive(t[n], r[n]) : t[n] = r[n]
                } catch (e) {
                    t[n] = r[n]
                }
                return t
            }, isArrayEqual: function (e, t) {
                if (e === t) return !0;
                if (null === e || null === t) return !1;
                if (e.length !== t.length) return !1;
                for (var r = 0; r < e.length; r++) if (e[r] !== t[r]) return !1;
                return !0
            }, isEqual: function (e, t) {
                if (null === e && e === t) return !0;
                if (_typeof(e) === _typeof(t)) switch (_typeof(e)) {
                    case"number":
                    case"string":
                    case"boolean":
                        return e === t;
                    case"object":
                        if (DTDHelpers.isArray(e) && DTDHelpers.isArray(t)) return DTDHelpers.isArrayEqual(e, t);
                        if (DTDHelpers.isObject(e) && DTDHelpers.isObject(t)) return JSON.stringify(e) === JSON.stringify(t)
                }
                return !1
            }, toTitle: function (e) {
                return e.charAt(0).toLowerCase() + e.slice(1)
            }, hasKey: function (e, t) {
                return Object.prototype.hasOwnProperty.call(t, e)
            }, getOS: function (e) {
                var t = {name: void 0, version: void 0};
                return mapper.rgx.call(t, e, regexes.os), t
            }, isBot: function (r) {
                return !!DTDHelpers.findObj(function (e, t) {
                    return r.includes(e)
                }, regexes.bot)
            }
        }, DTDApplicationInfo = function () {
            "use strict";

            function e() {
                Object.defineProperty(this, "applicationKey", {
                    get: function () {
                        return t
                    }
                }), Object.defineProperty(this, "apiUrl", {
                    get: function () {
                        return n
                    }, set: function (e) {
                        n = e
                    }
                }), Object.defineProperty(this, "testName", {
                    get: function () {
                        return o
                    }, set: function (e) {
                        o = e
                    }
                }), Object.defineProperty(this, "isTestingBuild", {
                    get: function () {
                        return i
                    }, set: function (e) {
                        i = e
                    }
                })
            }

            var t, r = null, n = "https://dataapi.devtodev.com/", o = null, i = !1, a = DTDHelpers;
            e.prototype.setApplicationKey = function (e) {
                t = e
            };

            function s(e) {
                return i && !a.isNullOrEmptyString(o) && e.unshift("TEST=".concat(o)), "".concat(n).concat(e.join("/"))
            }

            return e.prototype.getExperimentsUrl = function () {
                return s(["v2", "remoteconfig", "experiments"])
            }, e.prototype.getExperimentOfferUrl = function () {
                return s(["v2", "remoteconfig", "offer"])
            }, e.prototype.getConfigUrl = function () {
                return s(["v2", "analytics", "config"])
            }, e.prototype.getIdentificationUrl = function () {
                return s(["v2", "analytics", "identification"])
            }, e.prototype.getReportUrl = function () {
                return s(["v2", "analytics", "report"])
            }, e.prototype.getSdkVersion = function () {
                return "2.0"
            }, e.prototype.getSdkCodeVersion = function () {
                return "20"
            }, {
                getInstance: function () {
                    return r = r || new e
                }
            }
        }(), DTDLogger = function () {
            "use strict";

            function r(e, t, r) {
                if (a) {
                    var n = "[Devtodev] ";
                    if (o.isNullOrEmpty(r) || (t = o.format(t, r)), i[a] & e) switch (e) {
                        case i.Error:
                            console.error(n + t);
                            break;
                        case i.Warning:
                            console.warn(n + t);
                            break;
                        case i.Info:
                            console.info(n + t);
                            break;
                        case i.Debug:
                            console.log(n, t)
                    }
                }
            }

            function e() {
            }

            var t = null, o = DTDHelpers, i = {No: 0, Error: 1, Warning: 3, Info: 7, Debug: 15}, a = "No";
            return e.prototype.setLogLevel = function (e) {
                o.isNullOrEmpty(e) || o.isNullOrEmpty(i[e]) ? this.warning(DTDKeys.Log.SetLogNotValid, {type: e}) : a = e
            }, e.prototype.getLogLevel = function () {
                return a
            }, e.prototype.warning = function (e, t) {
                r(i.Warning, e, t)
            }, e.prototype.info = function (e, t) {
                r(i.Info, e, t)
            }, e.prototype.error = function (e, t) {
                r(i.Error, e, t)
            }, e.prototype.debug = function (e, t) {
                r(i.Debug, e, t)
            }, {
                getInstance: function () {
                    return t = t || new e
                }
            }
        }(), DTDKeys = {
            ApplicationKey: "application key",
            DeviceId: "device id",
            PrevDeviceId: "prev device id",
            UserId: "user id",
            ForceRemove: "forceRemove",
            Action: "Action",
            Properties: "properties",
            State: "state",
            Log: {
                NullOrEmpty: "{name} can't be null or empty",
                SetLogNotValid: "Invalid log level: {type}. Avaliable levels: [No, Error, Warning, Info, Debug]",
                TrackingDisabled: "Tracking was disabled for this device",
                AppVersionChanged: "App version changed",
                RequestSent: "Events report has been sent successfully",
                RequestFailed: "Events report hasn't been sent. Error: {error}",
                StringFieldNotValid: 'The "{name}" field value of the string must be between 1 and {max}',
                NumberFieldNotValidRange: 'The "{name}" field value must be a number between {min} and {max}',
                NumberFieldNotValidPositive: 'The "{name}" field must be a positive {type}',
                TypeNotValid: "{name} value must be an {type}.",
                ParameterNameWrongType: "{name} parameter name must be a {type}",
                ParameterNameWrongTypeNull: "{name} parameter name can't be null or empty",
                ParameterValueWrongType: "{name} parameter value must be a {type}",
                ParameterValueWrongTypeNull: "{name} parameter value can't be null or empty",
                NumberKeyNotValid: 'The "{name}" field value must be a number between {min} and {max}',
                StringFieldNotLonger: '"{name}" field value cannot be longer than {max} characters. The value has been shortened',
                StringKeyNotLonger: 'The key name "{name}" cannot be longer than {max} characters. The key name has been shortened',
                StringKeyValueNotLonger: 'The "{name}" field value cannot be longer than {max} characters. The key name has been shortened',
                NotInitialisation: "DTDAnalytics SDK not initialized",
                InitialisationSuccessful: "DTDAnalytics SDK is initialised with key '{application}' and version '{version}'",
                InitialisationFail: "DTDAnalytics SDK is not initialised with key '{application}' and version '{version}'",
                ReInitialisationFail: "DTDAnalytics SDK is not re-initialised with key '{application}' and version '{version}'",
                ReInitialisationSuccessful: "DTDAnalytics SDK is re-initialised with key '{application}' and version '{version}'",
                CurrencyTypeNotValid: '"{name}" must contain alphabetic code according to ISO 4217. For example: USD, EUR, RUB',
                LocalStorageNotAvailable: "Access to local storage not available",
                CookiesNotAvailable: "Access to cookies not available",
                StorageNotAvailable: "Access to storage not available",
                Excluded: "All {name} events are excluded on the server",
                Added: 'Event "{event}" was added',
                SetLevelSuccessful: 'Level "{level}" is set successfully for user {user}',
                StepDuplicate: "This tutorial step({step}) has already been sent. Skipped",
                StepIncorrect: "A tutorial step can include only predefined values: 0(Skipped), -1(Start), -2(Finish), and it can also be a positive integer",
                TransactionCheater: "User marked as cheater. Transaction is ignored",
                TransactionIdDuplicate: "Duplicate order ID",
                AccrualTypeNotValid: "Accrual type can include only predefined values: 0(earned), \n            1(bought), and it can also be a positive value",
                SetAppSuccessful: 'App version "{version}" is set successfully',
                ParametersExceeded: "You have exceeded the maximum number of parameters of the event ({name}).",
                ParameterWrongType: "Incorrect parameter type for key '{key}'. It's can be only String and Numbers.",
                SetUserSuccessful: "User {name} is activated successfully",
                SetUserSuccessfulNotValid: "Can't activated user.",
                RenameUserSuccessful: "User '{oldName}' has beed renamed to '{newName}'",
                RenameUserNewExist: "Can't rename user '{oldName}'. Name '{newName}' already exists",
                AlreadySent: "Referral data of the current user is already sent",
                ProgressionStart: "Progression event '{name}' is started",
                FinishFailStart: "Progression event '{name}' can't be finished. There is no active Progression event '{name}'",
                ProgressionFinish: "Progression event '{name}' is finished",
                SetPeopleWrongType: 'Key "{key}" must be a "{type}" type',
                SetPeopleGenderWrong: "Gender can only be specified with a predefined value: 0 (Unknown), 1 (Male), 2 (Female)",
                SetPeopleSuccessful: "User data has been set successfully",
                UnsetPeopleSuccessful: "User data has been successfully removed from the user profile",
                ClearSuccessful: "All user data has been cleared successfully"
            },
            People: {
                Age: "age",
                Name: "name",
                Email: "email",
                Phone: "phone",
                Photo: "photo",
                Gender: "gender",
                Cheater: "cheater",
                Properties: "properties",
                Action: {Set: "Set", Unset: "Unset", Clear: "Clear", SetOnce: "SetOnce"}
            },
            Identifiers: {
                DeviceId: "DeviceId",
                PreviousDeviceId: "PreviousDeviceId",
                CrossPlatformId: "UserId",
                PreviousCrossPlatformId: "PreviousUserId",
                BackendDeviceId: "DevtodevId",
                BackendCrossPlatformId: "CrossPlatformDevtodevId",
                RegistrationDate: "RegistrationDate"
            },
            SdkConfig: {},
            RemoteConfig: {
                ExperimentsSettings: {
                    Key: "experiments",
                    Id: "id",
                    Group: "group",
                    Conditions: "conditions",
                    Parameters: "parameters",
                    CompletionDate: "completionDate",
                    IsTesting: "isTesting"
                },
                UserProperties: {Key: "userProperties", Country: "country"},
                Callback: "Callback",
                ConfigKey: "ConfigKey",
                Backend: {Country: "country", Level: "level", PayingUser: "payingUser"},
                Action: {
                    SetDefaultsConfig: "SetDefaultsConfig",
                    GetDefaultsConfig: "GetDefaultsConfig",
                    GetRemoteConfig: "GetRemoteConfig",
                    GetConfigKey: "GetConfigKey",
                    GetABTest: "GetABTest",
                    Fetch: "Fetch",
                    Activate: "Activate"
                }
            },
            User: {
                PrimaryId: "0",
                DeviceId: "1",
                UserId: "2",
                PrevUserId: "3",
                IsDummy: "4",
                BackendDeviceId: "5",
                BackendCrossPlatformId: "6",
                RegistrationDate: "7",
                LastForeground: "8",
                SessionLength: "9",
                SessionStarted: "10",
                TutorialSteps: "11",
                CurrentLevel: "12",
                ReferralSatus: "14",
                CurrencyAccrual: "15",
                Profile: "16",
                UpdateProfile: "17",
                UserRemoteConfig: "18",
                IsPayingUser: "19",
                UserExperiments: "20"
            },
            UserRemoteConfig: {Settings: "0", DefaultsConfig: "1", RemoteConfig: "2"},
            UserExperiments: {Configs: "0", Status: "1", Parameters: "2", IsTesting: "3", Time: "4"},
            Device: {TrackingAvailability: "0", DeviceId: "1", PrevDeviceId: "2", PrimaryId: "3", IsDummy: "4"},
            App: {DummyId: "0", AppVersion: "1", TransactionIds: "2"}
        }, DTDActionType = {
            SetCurrencyAccrual: "SetCurrencyAccrual",
            SetUserId: "SetUserId",
            SetAppVersion: "SetAppVersion",
            SetCurrentLevel: "SetCurrentLevel",
            GenerateEvent: "GenerateEvent",
            ReplaceUserId: "ReplaceUserId",
            SetProgressionEvent: "SetProgressionEvent",
            GetCurrentLevel: "GetCurrentLevel",
            GetDeviceId: "GetDeviceId",
            GetAppVersion: "GetAppVersion",
            GetUserId: "GetUserId",
            GetSdkVersion: "GetSdkVersion",
            GetTrackingStatus: "GetTrackingStatus",
            People: "People",
            RemoteConfig: "RemoteConfig",
            IsAcivated: "IsAcivated"
        }, DTDAccrualType = {0: "earned", 1: "bought"}, DTDProgressionState = {Start: "Start", Finish: "Finish"},
        DTDRemoteConfigSource = {Ending: "ending", Remote: "remote", Defaults: "defaults"},
        StringConverter = function () {
            "use strict";

            function e() {
            }

            var c = DTDLogger.getInstance(), t = DTDHelpers, l = DTDKeys, d = t.isNullOrEmpty, f = t.isString;
            return e.prototype.transformString = function (e) {
                return function (e, t) {
                    var r = 1 < arguments.length && void 0 !== t ? t : {};
                    if (!f(e) || d(e)) return e;
                    var n = e = e.toString(), o = r.max, i = r.toLowerCase, a = r.breaks, s = r.encode;
                    if (!d(o)) {
                        n = e.substring(0, o.value);
                        var u = o.msg || l.Log.StringFieldNotLonger;
                        n !== e && c.warning(u, {max: o.value, name: o.name || ""})
                    }
                    return !d(i) && i && (n = e.toLowerCase()) !== e && c.warning(l.Log.StringFieldNotLonger, {max: o.value}), !d(a) && a && (n = e.replace(/(\r\n|\n|\r)/g, "")) !== e && c.warning(l.Log.StringFieldNotLonger, {max: o.value}), !d(s) && s && (n = encodeURIComponent(n)), n
                }(e, 1 < arguments.length && void 0 !== arguments[1] ? arguments[1] : {})
            }, e
        }(), TypeValidator = function () {
            "use strict";

            function e() {
            }

            function d(e) {
                return !e
            }

            function f(e, t, r, n) {
                var o = 1 < arguments.length && void 0 !== t ? t : {}, i = 2 < arguments.length ? r : void 0,
                    a = 3 < arguments.length ? n : void 0, s = o[e] ? o[e] : i;
                c.warning(s, a)
            }

            function a(e, t, r) {
                var n = 2 < arguments.length && void 0 !== r ? r : {}, o = 0;
                p.isNullOrEmpty(e) && (o = 1);
                var i = d(o);
                return i || f(o, n.errors, g.NullOrEmpty, {name: t}), i
            }

            function s(e, t, r, n, o, i) {
                var a = 5 < arguments.length && void 0 !== i ? i : {}, s = a.isEqMax, u = g.NumberFieldNotValidRange, c = 0;
                s && p.isMoreOrEqualThan(e, o) ? c = 3 : !s && p.isMoreThan(e, o) && (c = 3);
                var l = d(c);
                return l || f(c, a.errors, u, {min: n, name: t, max: o, type: r}), l
            }

            function u(e, t, r, n, o, i) {
                var a = 5 < arguments.length && void 0 !== i ? i : {}, s = a.isEqMin, u = g.NumberFieldNotValidRange, c = 0;
                s && p.isLessOrEqualThan(e, n) ? (u = g.NumberFieldNotValidPositive, c = 4) : !s && p.isLessThan(e, n) && (c = 4);
                var l = d(c);
                return l || f(c, a.errors, u, {min: n, name: t, max: o, type: r}), l
            }

            var c = DTDLogger.getInstance(), p = DTDHelpers, g = DTDKeys.Log;
            return e.prototype.checkNull = function (e, t) {
                return a(e, t, 2 < arguments.length && void 0 !== arguments[2] ? arguments[2] : {})
            }, e.prototype.checkBoolean = function (e, t) {
                var r = 2 < arguments.length && void 0 !== arguments[2] ? arguments[2] : {}, n = a(e, t, r);
                return n = n && function (e, t, r) {
                    var n = 2 < arguments.length && void 0 !== r ? r : {}, o = 0;
                    p.isBoolean(e) || (o = 2);
                    var i = d(o);
                    return i || f(o, n.errors, g.TypeNotValid, {name: t, type: "boolean"}), i
                }(e, t, r)
            }, e.prototype.checkString = function (e, t) {
                var r = 2 < arguments.length && void 0 !== arguments[2] ? arguments[2] : {}, n = a(e, t, r);
                return n = n && function (e, t, r) {
                    var n = 2 < arguments.length && void 0 !== r ? r : {}, o = 0;
                    p.isString(e) || (o = 2);
                    var i = d(o);
                    return i || f(o, n.errors, g.TypeNotValid, {name: t, type: "string"}), i
                }(e, t, r)
            }, e.prototype.checkInteger = function (e, t, r, n) {
                var o = 4 < arguments.length && void 0 !== arguments[4] ? arguments[4] : {}, i = a(e, t, o);
                return i = i && function (e, t, r) {
                    var n = 2 < arguments.length && void 0 !== r ? r : {}, o = 0;
                    p.isInteger(e) || (o = 2);
                    var i = d(o);
                    return i || f(o, n.errors, g.TypeNotValid, {name: t, type: "integer"}), i
                }(e, t, o), n = n || Number.POSITIVE_INFINITY, i = (i = i && u(e, t, "integer", r, n, o)) && s(e, t, "integer", r, n, o)
            }, e.prototype.checkNumber = function (e, t, r, n) {
                var o = 4 < arguments.length && void 0 !== arguments[4] ? arguments[4] : {}, i = a(e, t, o);
                return i = i && function (e, t, r) {
                    var n = 2 < arguments.length && void 0 !== r ? r : {}, o = 0;
                    p.isNumber(e) || (o = 2);
                    var i = d(o);
                    return i || f(o, n.errors, g.TypeNotValid, {name: t, type: "number"}), i
                }(e, t, o), n = n || Number.POSITIVE_INFINITY, i = (i = i && u(e, t, "number", r, n, o)) && s(e, t, "number", r, n, o)
            }, e
        }(), DTDSystemInfo = function () {
            "use strict";

            function e() {
                try {
                    t = i.languageToISO639_1("undefined" != typeof navigator && (navigator.language || navigator.userLanguage)), n = window.screen.width + "x" + window.screen.height, r = i.getOS(this.getUserAgent())
                } catch (e) {
                    a.debug(e)
                }
            }

            var t, r, n, o = null, i = DTDHelpers, a = DTDLogger.getInstance(),
                s = window && window.navigator && window.navigator.userAgent ? window.navigator.userAgent : "";
            return e.prototype.getUserAgent = function () {
                return s
            }, e.prototype.getTimeZoneOffset = function () {
                return -60 * (new Date).getTimezoneOffset()
            }, e.prototype.getLanguage = function () {
                return t
            }, e.prototype.getOsName = function () {
                return r.name || ""
            }, e.prototype.getOsVersion = function () {
                return r.version || ""
            }, e.prototype.getDisplayResolution = function () {
                return n
            }, {
                getInstance: function () {
                    return o = o || new e
                }
            }
        }(), DTDEventKeys = {
            Code: "code",
            Parameters: "parameters",
            Level: "level",
            Events: "events",
            Timestamp: "timestamp",
            SessionId: "sessionId",
            InProgress: "inProgress",
            AppVersion: "appVersion",
            InExperiments: "inExperiments",
            SdkVersion: "sdkVersion",
            UserId: "userId",
            DeviceId: "deviceId",
            Language: "language",
            PrevUserId: "prevUserId",
            Id: "id",
            SdkCodeVersion: "sdkCodeVersion",
            ExcludeVersion: "excludeVersion",
            DeviceInfo: {
                Code: "di",
                OsVersion: "osVersion",
                Os: "os",
                UUID: "uuid",
                UserAgent: "userAgent",
                TimeZoneOffset: "timeZoneOffset",
                DisplayResolution: "displayResolution"
            },
            SessionStart: {Code: "ss"},
            UserEngagement: {Code: "ue", Length: "length"},
            Alive: {Code: "al"},
            SocialConnect: {Code: "sc", SocialNetwork: "socialNetwork"},
            SocialPost: {Code: "sp", SocialNetwork: "socialNetwork", PostReason: "postReason"},
            LevelUp: {Code: "lu", Level: "level", Spent: "spent", Earned: "earned", Bought: "bought", Balance: "balance"},
            TrackingAvailability: {Code: "ts", TrackingAllowed: "trackingAllowed"},
            Tutorial: {Code: "tr", Step: "step"},
            RealCurrencyPayment: {
                Code: "rp",
                Price: "price",
                OrderId: "orderId",
                ProductId: "productId",
                CurrencyCode: "currencyCode"
            },
            VirtualCurrencyPayment: {
                Code: "vp",
                PurchaseId: "purchaseId",
                PurchaseType: "purchaseType",
                PurchasePrice: "purchasePrice",
                PurchaseAmount: "purchaseAmount",
                PurchasePriceCurrency: "purchasePriceCurrency"
            },
            CustomEvent: {Code: "ce", Name: "name"},
            ProgressionEvent: {
                Source: "source",
                Difficulty: "difficulty",
                Success: "success",
                Duration: "duration",
                Code: "pe",
                Name: "name",
                Spent: "spent",
                Earned: "earned"
            },
            Referrer: {
                Code: "rf",
                Term: "term",
                Medium: "medium",
                Source: "source",
                Content: "content",
                Campaign: "campaign"
            },
            CurrencyAccrual: {
                Code: "ca",
                CurrencyName: "currencyName",
                CurrencyAmount: "currencyAmount",
                AccrualType: "accrualType",
                CurrencySource: "currencySource"
            },
            People: {Code: "pl", Parameters: "parameters"}
        }, DTDEventValidator = function () {
            "use strict";

            function e(e, t, r) {
                n = t, u = e, c = r, l = new TypeValidator
            }

            function a(e) {
                return !(!n.getTrackingStatus() && !e) || (d.warning(g.TrackingDisabled), !1)
            }

            function s(e) {
                var t = !0, r = u.excludeEvents, n = e[f.Code];
                if (!p.isNullOrEmpty(r) && !p.isUndefined(r[n])) if (0 === r[n].length && (t = !1), t && n === f.Profile.Code) t = -1 === r[n].indexOf(e.key); else if (t && p.isNullOrEmpty(i[n])) {
                    var o = i[n];
                    t = -1 === r[n].indexOf(e[o])
                }
                return t || d.warning(g.Excluded, {name: n}), t
            }

            var t, n, u, c, l, d = DTDLogger.getInstance(), f = DTDEventKeys, r = DTDActionType, p = DTDHelpers,
                g = DTDKeys.Log,
                i = (_defineProperty(t = {}, f.SocialConnect.Code, f.SocialConnect.SocialNetwork), _defineProperty(t, f.SocialPost.Code, f.SocialPost.SocialNetwork), _defineProperty(t, f.Tutorial.Code, f.Tutorial.Step), _defineProperty(t, f.CustomEvent.Code, f.CustomEvent.Name), _defineProperty(t, f.VirtualCurrencyPayment.Code, f.VirtualCurrencyPayment.PurchaseId), _defineProperty(t, f.ProgressionEvent.Code, f.ProgressionEvent.Name), t);
            return e.prototype.canGenerateEvent = function (e) {
                var t = e[f.Code];
                if (a(t === f.TrackingAvailability.Code)) switch (t) {
                    case f.SocialConnect.Code:
                        return function (e) {
                            var t = s(e);
                            if (t) {
                                var r = e[f.SocialConnect.SocialNetwork];
                                t = l.checkString(r, "socialNetwork")
                            }
                            return t
                        }(e);
                    case f.Tutorial.Code:
                        return function (e) {
                            var t = s(e);
                            if (t) {
                                var r = e[f.Tutorial.Step];
                                return (t = l.checkInteger(r, "Step")) && p.isLessThan(r, 1) && -1 === [0, -1, -2].indexOf(r) && (d.warning(g.StepIncorrect), t = !1), t && -1 < c.getTutorialStep().indexOf(r) && (d.warning(g.StepDuplicate, {step: r}), t = !1), t
                            }
                        }(e);
                    case f.SocialPost.Code:
                        return function (e) {
                            var t = s(e);
                            if (t) {
                                var r = e[f.SocialPost.SocialNetwork], n = e[f.SocialPost.PostReason];
                                t = (t = l.checkString(r, "socialNetwork")) && l.checkString(n, "reason")
                            }
                            return t
                        }(e);
                    case f.RealCurrencyPayment.Code:
                        return function (e) {
                            var t = s(e);
                            if (t) {
                                var r = e[f.RealCurrencyPayment.OrderId], n = e[f.RealCurrencyPayment.ProductId],
                                    o = e[f.RealCurrencyPayment.CurrencyCode], i = e[f.RealCurrencyPayment.Price];
                                c.isCheater() && (d.warning(g.TransactionCheater), t = !1), t = (t = (t = t && l.checkString(r, "orderId")) && l.checkString(n, "productId")) && l.checkString(o, "currencyCode"), p.isValueISO4217(o) || (t = !1, d.warning(g.CurrencyTypeNotValid, {name: "currencyCode"})), (t = t && l.checkNumber(i, "price", 0, Number.POSITIVE_INFINITY, {isEqMin: !0})) && -1 < u.getTransactionIds().indexOf(r) && (d.warning(g.TransactionIdDuplicate), t = !1)
                            }
                            return t
                        }(e);
                    case f.VirtualCurrencyPayment.Code:
                        return function (e) {
                            var t = s(e);
                            if (t) {
                                var r = e[f.VirtualCurrencyPayment.PurchaseId],
                                    n = e[f.VirtualCurrencyPayment.PurchaseType],
                                    o = e[DTDEventKeys.VirtualCurrencyPayment.PurchaseAmount],
                                    i = e[f.VirtualCurrencyPayment.PurchasePrice];
                                t = (t = (t = (t = l.checkString(r, "purchaseId")) && l.checkString(n, "purchaseType")) && l.checkNull(i, "resources")) && l.checkInteger(o, "purchaseAmount", 0, Number.POSITIVE_INFINITY, {isEqMin: !0})
                            }
                            return t
                        }(e);
                    case f.CustomEvent.Code:
                        return function (e) {
                            var t = s(e);
                            if (t) {
                                var r = e[f.CustomEvent.Name];
                                t = l.checkString(r, "name")
                            }
                            return t
                        }(e);
                    case f.LevelUp.Code:
                        return function (e) {
                            var t = s(e);
                            if (t) {
                                var r = e[f.LevelUp.Level];
                                t = c.getCurrentLevel() !== r && l.checkInteger(r, "Level", 1, Number.POSITIVE_INFINITY)
                            }
                            return t
                        }(e);
                    case f.Referrer.Code:
                        return function (e) {
                            var t = s(e);
                            return t && ((t = c.isActiveReferralStatus()) ? t = !!p.findObj(function (e, t) {
                                return t !== f.Code
                            }, e) : d.warning(g.AlreadySent)), t
                        }(e);
                    default:
                        return !0
                }
                return !1
            }, e.prototype.canSetData = function (e, t) {
                if (a()) switch (e) {
                    case r.SetCurrentLevel:
                        return function (e) {
                            var t = e[f.Level], r = a();
                            return r = r && l.checkInteger(t, "Level", 1, Number.POSITIVE_INFINITY)
                        }(t);
                    case r.SetCurrencyAccrual:
                        return function (e) {
                            var t = a();
                            if (t) {
                                var r = e[f.CurrencyAccrual.CurrencySource], n = e[f.CurrencyAccrual.AccrualType],
                                    o = e[f.CurrencyAccrual.CurrencyAmount], i = e[f.CurrencyAccrual.CurrencyName];
                                (t = (t = (t = l.checkString(i, "currencyName")) && l.checkString(r, "currencySource")) && l.checkString(r, "currencySource")) && p.isNullOrEmpty(DTDAccrualType[n]) && (d.warning(g.AccrualTypeNotValid), t = !1), t = (t = t && l.checkInteger(n, "accrualType", 0, 1)) && l.checkInteger(o, "currencyAmount", Number.NEGATIVE_INFINITY, Number.POSITIVE_INFINITY)
                            }
                            return t
                        }(t);
                    case r.SetAppVersion:
                        return function (e) {
                            var t = a();
                            if (t) {
                                var r = e[f.AppVersion];
                                t = l.checkString(r, "appVersion")
                            }
                            return t
                        }(t);
                    case r.ReplaceUserId:
                        return function (e) {
                            var t = a();
                            if (t) {
                                var r = e[f.PrevUserId], n = e[f.UserId];
                                t = (t = (t = (t = l.checkString(r, "prevUserId")) && l.checkString(n, "userId")) && l.checkString(r, "prevUserId")) && l.checkString(n, "userId")
                            }
                            return t
                        }(t);
                    case r.SetProgressionEvent:
                        return function (e) {
                            var t = s(e);
                            if (t) {
                                var r = e[f.ProgressionEvent.Name];
                                t = l.checkString(r, "name")
                            }
                            return t
                        }(t);
                    case r.People:
                        return s(t);
                    default:
                        return !0
                }
                return !1
            }, e
        }(), DTDEventConverter = function () {
            "use strict";

            function e(e) {
                a = e;
                var t = new StringConverter;
                c = t.transformString, l = new TypeValidator
            }

            function u(n, e, t, r) {
                var o = 2 < arguments.length && void 0 !== t ? t : 0, i = 3 < arguments.length && void 0 !== r ? r : 32,
                    a = {}, s = {
                        1: f.Log.ParameterNameWrongTypeNull,
                        2: f.Log.ParameterNameWrongType,
                        3: f.Log.ParameterNameWrongType
                    }, u = {
                        1: f.Log.ParameterValueWrongTypeNull,
                        2: f.Log.ParameterValueWrongType,
                        3: f.Log.ParameterValueWrongType
                    };
                return g(function (e, t) {
                    if (function (e, t) {
                        var r = !0;
                        return r = (r = r && l.checkString(e, n, {errors: s})) && l.checkInteger(t, n, o, void 0, {
                            isEqMin: !0,
                            errors: u
                        })
                    }(t, e)) {
                        var r = c(t, {max: {value: i, name: n}, msg: f.Log.StringKeyNotLonger, name: t});
                        a[r] = e
                    }
                }, e), a
            }

            function t(e) {
                var r = e[d.CustomEvent.Name], t = e[d.Parameters];
                r = c(r, {max: {value: 72, name: "name"}}), e[d.CustomEvent.Name] = r;
                var n = 0, o = {}, i = a.getEventParamsCount();
                return g(function (e, t) {
                    n < i ? function (e, t) {
                        var r = !0;
                        if (r = r && l.checkString(e, "Custom event", {errors: {1: f.Log.ParameterNameWrongTypeNull}})) {
                            var n = {
                                1: f.Log.ParameterValueWrongTypeNull,
                                2: f.Log.ParameterValueWrongType,
                                3: f.Log.ParameterValueWrongType
                            };
                            r = v(t) ? r && l.checkNumber(t, "Custom event", 0, void 0, {errors: n}) : r && l.checkString(t, "Custom event", {errors: n})
                        }
                        return r
                    }(t, e) && (t = c(t, {max: {value: 32, name: t}}), y(e) ? o[t] = c(e, {
                        max: {
                            value: 255,
                            name: t,
                            msg: DTDKeys.Log.StringKeyValueNotLonger
                        }
                    }) : o[t] = e, n++) : n === i && s.warning(f.Log.ParametersExceeded, {name: r})
                }, t), p(o) ? delete e[d.Parameters] : e[d.Parameters] = o, e
            }

            function r(e) {
                var t = e[f.State], r = e[d.ProgressionEvent.Name], n = {};
                return (n = t === DTDProgressionState.Start ? function (e) {
                    var t = e[d.ProgressionEvent.Difficulty], r = e[d.ProgressionEvent.Source], n = {};
                    return m("difficulty", e) && l.checkInteger(t, "difficulty", 0, void 0) && (n[d.ProgressionEvent.Difficulty] = t), m("source", e) && l.checkString(r, "source") && (n[d.ProgressionEvent.Source] = c(r, {
                        max: {
                            value: 40,
                            name: "source"
                        }
                    })), n
                }(e) : function (e) {
                    var t = {}, r = d.ProgressionEvent, n = e[r.Duration];
                    m("duration", e) && l.checkInteger(n, "duration", 0, void 0) && (t[r.Duration] = n);
                    var o = e[r.Success];
                    m(r.Success, e) && l.checkBoolean(o, "success", 0, void 0) && (t[r.Success] = o);
                    var i = r.Spent, a = u(i, e[i], 0, 24);
                    p(a) || (t[i] = a), i = r.Earned;
                    var s = u(i, e[i], 0, 24);
                    return p(s) || (t[i] = s), t
                }(e))[d.ProgressionEvent.Name] = c(r, {
                    max: {
                        value: 40,
                        name: "name"
                    }
                }), n[d.Code] = d.ProgressionEvent.Code, n
            }

            var a, c, l, s = DTDLogger.getInstance(), n = DTDHelpers, d = DTDEventKeys, f = DTDKeys, p = n.isNullOrEmpty,
                g = n.forEachObj, y = n.isString, v = n.isNumber, m = n.hasKey;
            return e.prototype.transformEvent = function (e) {
                switch (e[d.Code]) {
                    case d.DeviceInfo.Code:
                        return function (r) {
                            return g(function (e, t) {
                                switch (e) {
                                    case d.DeviceInfo.Code:
                                    case d.DeviceInfo.TimeZoneOffset:
                                        break;
                                    default:
                                        l.checkString(r[e], t) || delete r[e]
                                }
                            }, d.DeviceInfo), r
                        }(e);
                    case d.SocialConnect.Code:
                        return function (e) {
                            var t = e[d.SocialConnect.SocialNetwork];
                            return e[d.SocialConnect.SocialNetwork] = c(t, {max: {value: 24, name: "Social network"}}), e
                        }(e);
                    case d.SocialPost.Code:
                        return function (e) {
                            var t = e[d.SocialPost.SocialNetwork], r = e[d.SocialPost.PostReason];
                            return e[d.SocialPost.SocialNetwork] = c(t, {
                                max: {
                                    value: 24,
                                    name: "Social network"
                                }
                            }), e[d.SocialPost.PostReason] = c(r, {max: {value: 32, name: "Post reason"}}), e
                        }(e);
                    case d.RealCurrencyPayment.Code:
                        return function (e) {
                            var t = e[d.RealCurrencyPayment.OrderId], r = e[d.RealCurrencyPayment.ProductId],
                                n = e[d.RealCurrencyPayment.CurrencyCode];
                            return e[d.RealCurrencyPayment.OrderId] = c(t, {
                                max: {
                                    value: 65,
                                    name: "orderId"
                                }
                            }), e[d.RealCurrencyPayment.ProductId] = c(r, {
                                max: {
                                    value: 255,
                                    name: "productId"
                                }
                            }), e[d.RealCurrencyPayment.CurrencyCode] = c(n, {max: {value: 24, name: "currencyCode"}}), e
                        }(e);
                    case d.VirtualCurrencyPayment.Code:
                        return function (e) {
                            var t = e[d.VirtualCurrencyPayment.PurchaseId], r = e[d.VirtualCurrencyPayment.PurchaseType],
                                n = e[d.VirtualCurrencyPayment.PurchasePrice];
                            return e[d.VirtualCurrencyPayment.PurchasePrice] = u("PurchasePrice", n), e[d.VirtualCurrencyPayment.PurchaseId] = c(t, {
                                max: {
                                    value: 32,
                                    name: "purchaseId"
                                }
                            }), e[d.VirtualCurrencyPayment.PurchaseType] = c(r, {max: {value: 96, name: "purchaseType"}}), e
                        }(e);
                    case d.CustomEvent.Code:
                        return t(e);
                    case d.LevelUp.Code:
                        return function (e) {
                            var t = e[DTDEventKeys.LevelUp.Bought];
                            t = u("Bought", t);
                            var r = e[DTDEventKeys.LevelUp.Spent];
                            r = u("Spent", r, 24);
                            var n = e[DTDEventKeys.LevelUp.Earned];
                            n = u("Earned", n, 24);
                            var o = e[DTDEventKeys.LevelUp.Balance];
                            return o = u("Balance", o, Number.MIN_VALUE, 24), p(t) ? delete e[DTDEventKeys.LevelUp.Bought] : e[DTDEventKeys.LevelUp.Bought] = t, p(r) ? delete e[DTDEventKeys.LevelUp.Spent] : e[DTDEventKeys.LevelUp.Spent] = r, p(n) ? delete e[DTDEventKeys.LevelUp.Earned] : e[DTDEventKeys.LevelUp.Earned] = n, p(o) ? delete e[DTDEventKeys.LevelUp.Balance] : e[DTDEventKeys.LevelUp.Balance] = o, e
                        }(e);
                    case d.Referrer.Code:
                        return function (e) {
                            var r = [d.Referrer.Source, d.Referrer.Medium, d.Referrer.Content, d.Referrer.Campaign, d.Referrer.Term, d.Code],
                                n = {};
                            return g(function (e, t) {
                                -1 < r.indexOf(t) && !p(e) && (n[t] = e)
                            }, e), n
                        }(e);
                    default:
                        return e
                }
            }, e.prototype.transformData = function (e, t) {
                switch (e) {
                    case DTDActionType.SetCurrencyAccrual:
                        return function (e) {
                            var t = e[d.CurrencyAccrual.CurrencySource], r = e[d.CurrencyAccrual.CurrencyName];
                            return e[d.CurrencyAccrual.CurrencySource] = c(t, {
                                max: {
                                    value: 32,
                                    name: "currencySource"
                                }
                            }), e[d.CurrencyAccrual.CurrencyName] = c(r, {max: {value: 24, name: "currencyName"}}), e
                        }(t);
                    case DTDActionType.SetAppVersion:
                        return function (e) {
                            var t = e[d.AppVersion];
                            return e[d.CurrencyAccrual.CurrencySource] = c(t, {max: {value: 32, name: "currencySource"}}), e
                        }(t);
                    case DTDActionType.ReplaceUserId:
                        return function (e) {
                            var t = e[d.PrevUserId], r = e[d.UserId];
                            return e[d.PrevUserId] = c(t, {
                                max: {
                                    value: 64,
                                    name: "prevUserId"
                                }
                            }), e[d.UserId] = c(r, {max: {value: 64, name: "userId"}}), e
                        }(t);
                    case DTDActionType.SetProgressionEvent:
                        return r(t);
                    case DTDActionType.SetUserId:
                        return function (e) {
                            var t = e[f.UserId], r = e[f.DeviceId];
                            return e[f.UserId] = c(t, {
                                max: {value: 64, name: "userId"},
                                break: !0
                            }), e[f.DeviceId] = c(r, {max: {value: 64, name: "deviceId"}, break: !0}), e
                        }(t);
                    default:
                        return t
                }
            }, e
        }(), RequestStructure = function () {
            var r = DTDHelpers;

            function e(e) {
                this._headers = r.filterObj(function (e) {
                    return !r.isNullOrEmpty(e)
                }, e), this._body = {}, this._urlParams = {}, this._url = null
            }

            return e.prototype.addToUrl = function (e, t) {
                r.isNullOrEmpty(t) || (this._urlParams[e] = t)
            }, e.prototype.setUrl = function (e) {
                r.isNullOrEmpty(e) || (this._url = e)
            }, e.prototype.addBody = function (e, t) {
                r.isNullOrEmpty(t) || (this._body[e] = t)
            }, e.prototype.getHeader = function () {
                return this._headers
            }, e.prototype.getBody = function () {
                return this._body
            }, e.prototype.getUrl = function () {
                return r.buildUrl(this._url, this._urlParams)
            }, e
        }(), DTDNetworkRequest = function () {
            function e(e, t, r) {
                this.request = null, this.key = e, this.requestStructure = t, this.callback = r
            }

            function t(e, n, t) {
                var o, r, i,
                    a = 2 < arguments.length && void 0 !== t ? t : {sync: !1, gzip: !0, method: "POST", timeout: 3e3},
                    s = function (e, t, r) {
                        void 0 !== n && n(e, t, r)
                    }, u = e.getUrl(), c = !!window.XDomainRequest;

                function l(e) {
                    return clearTimeout(f), e instanceof Error || (e = new Error("" + (e || "Unknown XMLHttpRequest Error"))), e.statusCode = 0, null !== n && n(this.key), console.log("!errorFunc!", e), s(e.statusCode, {}, e)
                }

                function d() {
                    if (!i) {
                        var e, t;
                        clearTimeout(f), console.log("!loadFunc!", o);
                        var r = null;
                        return 0 !== (e = c && void 0 === o.status ? 200 : 1223 === o.status ? 204 : o.status) ? t = function () {
                            var e;
                            e = o.response ? o.response : o.responseText;
                            try {
                                e = JSON.parse(e)
                            } catch (e) {
                            }
                            return e
                        }() : r = new Error("Internal XMLHttpRequest Error"), s(e, t, r)
                    }
                }

                var f, p = (o = c ? new window.XDomainRequest : new window.XMLHttpRequest).method = a.method || "GET",
                    g = e.getBody(), y = o.headers = e.getHeader() || {}, v = !!a.sync;
                if (o.withCredentials = !0, o.timeout = 3e3, "GET" !== p && "HEAD" !== p) {
                    var m = "application/json";
                    a.gzip ? (g = iampakopako.deflate(JSON.stringify(g), {
                        gzip: !0,
                        level: 9
                    }), m = "application/gzip") : g = JSON.stringify(g), y["content-type"] || y["Content-Type"] || (y["Content-Type"] = m)
                }
                if (o.onreadystatechange = function () {
                    4 === o.readyState && setTimeout(d, 0)
                }, c && (o.onload = d), o.onerror = l, o.onprogress = function () {
                }, o.onabort = function () {
                    i = !0
                }, o.ontimeout = l, o.open(p, u, !v), v || (o.withCredentials = !!a.withCredentials), !v && 0 < a.timeout && (f = setTimeout(function () {
                    if (!i) {
                        i = !0, o.abort("timeout");
                        var e = new Error("XMLHttpRequest timeout");
                        e.code = "ETIMEDOUT", l(e)
                    }
                }, a.timeout)), o.setRequestHeader) for (r in y) y.hasOwnProperty(r) && o.setRequestHeader(r, y[r]); else if (a.headers && !DTDHelpers.isNullOrEmpty(a.headers)) throw new Error("Headers cannot be set on an XDomainRequest object");
                return o.send("function" == typeof ArrayBufferView ? g : g.buffer), o
            }

            return e.prototype.run = function (e) {
                DTDHelpers.isNullOrEmpty(this.request) || this.request.abort(), this.request = t(this.requestStructure, this.callback, e)
            }, e.prototype.abort = function (e) {
                DTDHelpers.isNullOrEmpty(this.reques) || this.request.abort()
            }, e
        }(), DTDRequestManager = function () {
            var o = DTDHelpers, t = o.forEachObj;

            function e() {
                _tasks = {}
            }

            return _stopTask = function (e) {
                o.isNullOrEmpty(_tasks[e]) || delete _tasks[e]
            }, e.prototype.addTask = function (e, t, r, n) {
                o.isNullOrEmpty(_tasks[e]) || _tasks[e].abort(), _tasks[e] = new DTDNetworkRequest(e, t, function (n, o) {
                    return function (e, t, r) {
                        n(e, t, r), _stopTask(o)
                    }
                }.bind(this)(r, e)), _tasks[e].run(n)
            }, e.prototype.removeTask = function (e) {
                o.isNullOrEmpty(_tasks[e]) || (_tasks[e].abort(), delete _tasks[e])
            }, e.prototype.removeAllTask = function (e) {
                t(function (e, t) {
                    _tasks[t].abort()
                }, _tasks), _tasks = {}
            }, e
        }(), DTDConfigManager = function () {
            var n, r, o, i;

            function e(e, t) {
                n = e, r = t
            }

            function t(e, t, r) {
                DTDHelpers.isNullOrEmpty(r) && 200 === e ? i(t) : n.addSchedulerTask("config", 2e3, _fetch)
            }

            return _fetch = function () {
                r.addTask("getConfig", o, t)
            }, e.prototype.fetch = function (e, t) {
                o = e, i = t, _fetch()
            }, e
        }(), DTDIdentifiersRequest = function () {
            var n, t, o = 1, e = null, i = !1;

            function r(e) {
                t = e
            }

            var a = function (e, t) {
                n.addBody("attempt", t);
                var r = DTDApplicationInfo.getInstance().getIdentificationUrl();
                r = DTDHelpers.buildUrl(r, {appId: e})
            };
            return r.prototype.fetch = function (e, t, r) {
                this.abort(), i = !0, r, n = t, a(e, o)
            }, r.prototype.isRuning = function () {
                return i
            }, r.prototype.abort = function () {
                DTDHelpers.isNullOrEmpty(e) || t.ignoreTimer(e.id)
            }, r
        }(), RemoteConfigRequest = function () {
            var n, o = DTDApplicationInfo.getInstance();

            function e(e, t) {
                e, n = t
            }

            function i(n) {
                return function (e, t, r) {
                    DTDHelpers.isNullOrEmpty(r) && 200 === e ? n(t) : DTDLogger.getInstance("Error while sending experiments request: (".concat(r, ")"))
                }
            }

            return e.prototype.fetch = function (e, t, r) {
                t.addToUrl("appId", e), t.setUrl(o.getExperimentsUrl()), n.addTask("getRConfig", t, i(r))
            }, e.prototype.offer = function (e, t, r) {
                t.addToUrl("appId", e), t.setUrl(o.getExperimentOfferUrl()), n.addTask("offer", t, i(r))
            }, e
        }(), DTDRequestCoordinator = function () {
            function e(e, t, r) {
                C = e, DTDHelpers.getCurrentTimeMs(), _timeManager = t, n = r
            }

            function _(r) {
                var n = [];
                return U(function (e, t) {
                    n.push(r[e])
                }, ["language", "sdkCodeVersion", "sdkVersion", "userId"]), n.join("_")
            }

            function P(r) {
                var n = {};
                return U(function (e, t) {
                    A(r[e]) || (n[e] = r[e])
                }, ["language", "sdkCodeVersion", "sdkVersion"]), n
            }

            function T() {
                _timeManager.removeSchedulerTask("repeatSend"), o()
            }

            var C, S, n, I = DTDEventKeys, b = DTDKeys, w = {}, k = DTDLogger.getInstance(),
                t = DTDApplicationInfo.getInstance(), E = DTDHelpers.toTitle, A = DTDHelpers.isNullOrEmpty,
                U = DTDHelpers.forEach, o = function () {
                    var r, e = C.getEvent().get(), t = [], n = [], o = e.length, i = {};
                    if (0 < o && !S) {
                        for (var a, s, u, c = 0, l = new RemoteConfigUser, d = 0; d < o; d++) {
                            var f = _objectSpread({}, e[d]), p = f[I.UserId];
                            if (A(a) || p === a && c < 10) {
                                if (A(a)) if (s = C.getStorage().getUserById(p), A(s)) i[f[DTDEventKeys.Id]] = 1; else {
                                    var g, y = s[b.User.DeviceId];
                                    if (u = y ? C.getStorage().getDeviceById(y) : null, A(s) || A(u) || !1 === u[b.Device.TrackingAvailability]) {
                                        i[f[DTDEventKeys.Id]] = 1;
                                        continue
                                    }
                                    if (l.setUser(s), l.thereAreNoNonfirmedExperiments() && l.getTimeWait() < C.getApp().getAbTestStartTimeout() && l.getTimeWait() >= l.getTimeMin()) {
                                        _timeManager.hasSchedulerTask("repeatSend") || _timeManager.addSchedulerTask("repeatSend", l.getTimeWait(), T);
                                        continue
                                    }
                                    a = p, _defineProperty(g = {}, E(b.Identifiers.DeviceId), u[b.Device.DeviceId]), _defineProperty(g, E(b.Identifiers.PreviousDeviceId), u[b.Device.PrevDeviceId]), _defineProperty(g, E(b.Identifiers.CrossPlatformId), s[b.User.UserId]), _defineProperty(g, E(b.Identifiers.PreviousCrossPlatformId), s[b.User.PrevUserId]), _defineProperty(g, E(b.Identifiers.BackendDeviceId), s[b.User.BackendDeviceId]), _defineProperty(g, E(b.Identifiers.BackendCrossPlatformId), s[b.User.BackendCrossPlatformId]), _defineProperty(g, E(b.Identifiers.RegistrationDate), s[b.User.RegistrationDate]), _defineProperty(g, "packages", []), r = g
                                }
                                var v = _(f);
                                A(n[v]) && (n[v] = P(f), n[v].events = [], t.push(v)), f[DTDEventKeys.Events].forceRemove ? (i[f[DTDEventKeys.Id]] = 1, delete f[DTDEventKeys.Events].forceRemove) : w[f[DTDEventKeys.Id]] = 1;
                                var m = f[DTDEventKeys.Events];
                                l.inExperiments() && f[DTDEventKeys.Events][DTDEventKeys.Timestamp] >= l.getTimeStart() && (m[DTDEventKeys.InExperiments] = l.get()), n[v].events.push(m), c++
                            }
                        }
                        if (0 < c) {
                            var h;
                            U(function (e, t) {
                                r.packages.push(n[e])
                            }, t), i = x(i), S = !0;
                            var D = new RequestStructure((_defineProperty(h = {}, b.Identifiers.DeviceId, u[b.Device.DeviceId]), _defineProperty(h, b.Identifiers.PreviousDeviceId, u[b.Device.PrevDeviceId]), _defineProperty(h, b.Identifiers.CrossPlatformId, s[b.User.UserId]), _defineProperty(h, b.Identifiers.PreviousCrossPlatformId, s[b.User.PrevUserId]), _defineProperty(h, b.Identifiers.BackendDeviceId, s[b.User.BackendDeviceId]), _defineProperty(h, b.Identifiers.BackendCrossPlatformId, s[b.User.BackendCrossPlatformId]), h));
                            D.addBody("reports", [r]), k.debug(JSON.stringify(D.getBody())), N(D)
                        }
                    } else k.info(DTDKeys.Log.RequestSent)
                }, N = function (e) {
                    e.setUrl(t.getReportUrl()), e.addToUrl("appId", t.applicationKey), n.addTask("queue", e, function (e, t, r) {
                        DTDHelpers.isNullOrEmpty(r) && 200 === e ? (w = x(w), S = !1, o()) : _timeManager.hasSchedulerTask("repeatSend") || _timeManager.addSchedulerTask("repeatSend", 3e3, T)
                    })
                }, x = function (e) {
                    for (var t = C.getEvent().get(), r = [], n = 0; n < t.length && !A(e); n++) {
                        var o = t[n][DTDEventKeys.Id];
                        e[o] ? delete e[o] : r.push(t[n])
                    }
                    return C.getEvent().replace(r), {}
                };
            return e.prototype.sendEvents = function () {
                o()
            }, e
        }(), DTDCookies = function () {
            function e() {
            }

            var i = {};
            return e.prototype.isAvailable = function () {
                return void 0 !== window.navigator && void 0 !== window.navigator.cookieEnabled ? window.navigator.cookieEnabled : (window.document.cookie = "testcookie=test; max-age=10000", 1 !== (i = window.document.cookie).indexOf("testcookie=test") && (window.document.cookie = "testcookie=test; max-age=1", !0))
            }, e.prototype.set = function (e, t) {
                var r = new Date;
                r.setTime(Date.now() + 31536e7), window.document.cookie = e + "=" + t + "; expires=" + r.toGMTString() + "; path=/"
            }, e.prototype.get = function (e) {
                var t, r, n, o = null;
                for (i = window.document.cookie.split(";"), t = 0; t < i.length; t++) n = (r = i[t]).indexOf("="), DTDHelpers.trimmer(r.slice(0, n)) === e && (o = r.slice(n + 1));
                return o
            }, e.prototype.remove = function (e) {
                window.document.cookie = e + "=; expires='Thu, 01 Jan 1970 00:00:00 GMT'; path=/"
            }, e
        }(), DTDLocalStorage = function () {
            function e() {
            }

            return e.prototype.isAvailable = function () {
                var t = !1;
                try {
                    "localStorage" in window && null !== window.localStorage && (window.localStorage.setItem("test", "yes"), window.localStorage.removeItem("test"), t = !0)
                } catch (e) {
                    t = !1
                }
                return t
            }, e.prototype.set = function (e, t) {
                window.localStorage.setItem(e, t)
            }, e.prototype.get = function (e) {
                var t = window.localStorage.getItem(e);
                return t = "undefined" === t ? null : t
            }, e.prototype.remove = function (e) {
                window.localStorage.removeItem(e)
            }, e
        }(), DTDStorage = function () {
            var r, n = null, t = !1, o = {}, i = {}, a = {}, s = [], u = DTDHelpers, c = "app", l = "users", d = "devices",
                f = "event", p = DTDLogger.getInstance();

            function e() {
                var e = new DTDLocalStorage;
                e.isAvailable() ? (n = e, t = !0) : p.info(DTDKeys.Log.LocalStorageNotAvailable), t || ((e = new DTDCookies).isAvailable() ? (n = e, t = !0) : p.info(DTDKeys.Log.CookiesNotAvailable)), t || DTDLogger.getInstance().info(DTDKeys.Log.StorageNotAvailable)
            }

            function g(e, t) {
                var r = t.split("."), n = r.length;
                if (n) {
                    for (var o = e, i = 0; i < n && o; ++i) {
                        if (void 0 === o[r[i]]) {
                            o = null;
                            break
                        }
                        o = o[r[i]]
                    }
                    return o
                }
            }

            function y(e, t, r) {
                var n = t.split(".");
                if (n.length) return function e(t, r, n, o) {
                    if (o >= n.length) return r;
                    var i = n[o];
                    return _objectSpread({}, t, _defineProperty({}, i, e(t && t[i] ? t[i] : {}, r, n, o + 1)))
                }(e, r, n, 0)
            }

            function v() {
                return t
            }

            function m(e) {
                var t;
                if (v()) switch (e) {
                    case c:
                        t = u.generateKey([r, c], !1), n.set(t, u.prepareStoreData(o, !1));
                        break;
                    case l:
                        t = u.generateKey([r, l], !1), n.set(t, u.prepareStoreData(i, !1));
                        break;
                    case f:
                        t = u.generateKey([r, f], !1), n.set(t, u.prepareStoreData(s, !1));
                        break;
                    case d:
                        t = u.generateKey([r, d], !1), n.set(t, u.prepareStoreData(a, !1))
                }
            }

            return e.prototype.initialize = function (e) {
                r = e, v() && this.load()
            }, e.prototype.load = function () {
                if (v()) {
                    var e;
                    try {
                        e = u.generateKey([r, c], !1), o = (o = u.parseStoreData(n.get(e), !1)) || {}
                    } catch (e) {
                    }
                    try {
                        e = u.generateKey([r, f], !1), s = (s = u.parseStoreData(n.get(e), !1)) || []
                    } catch (e) {
                    }
                    try {
                        e = u.generateKey([r, l], !1), i = (i = u.parseStoreData(n.get(e), !1)) || {}
                    } catch (e) {
                    }
                    try {
                        e = u.generateKey([r, d], !1), a = (a = u.parseStoreData(n.get(e), !1)) || {}
                    } catch (e) {
                    }
                }
            }, e.prototype.getApp = function (e) {
                return g(o, e)
            }, e.prototype.saveApp = function (e, t) {
                o = y(o, e, t), m(c)
            }, e.prototype.getEvent = function (e) {
                return s
            }, e.prototype.insertEvent = function (e) {
                s.push(e), m(f)
            }, e.prototype.replaceEvent = function (e) {
                s = e, m(f)
            }, e.prototype.findPrimaryUserId = function (t) {
                var e = u.findObj(function (r, e) {
                    return !u.findObj(function (e, t) {
                        return r[t] !== e
                    }, t)
                }, i);
                return e ? e[DTDKeys.User.PrimaryId] : null
            }, e.prototype.getUserFieldById = function (e, t) {
                return g(i, "".concat(e, ".").concat(t))
            }, e.prototype.getUserById = function (e) {
                return g(i, "".concat(e))
            }, e.prototype.saveUserById = function (e, t) {
                i[e] = t, m(l)
            }, e.prototype.saveUserFieldById = function (e, t, r) {
                i = y(i, "".concat(e, ".").concat(t), r), m(l)
            }, e.prototype.findPrimaryDeviceId = function (t) {
                var e = u.findObj(function (r, e) {
                    return !u.findObj(function (e, t) {
                        return r[t] !== e
                    }, t)
                }, a);
                return e ? e[DTDKeys.Device.PrimaryId] : null
            }, e.prototype.getDeviceById = function (e) {
                return g(a, e)
            }, e.prototype.saveDeviceById = function (e, t) {
                a[e] = t, m(d)
            }, e.prototype.getDeviceFieldById = function (e, t) {
                return g(a, "".concat(e, ".").concat(t))
            }, e
        }(), DTDPeopleMerger = function () {
            var a, s, u, c, l = {}, d = DTDKeys, o = DTDHelpers, f = DTDLogger.getInstance(), i = o.isNullOrEmpty,
                p = o.forEachObj, g = o.isNumber,
                y = [d.People.Age, d.People.Name, d.People.Email, d.People.Phone, d.People.Photo, d.People.Gender, d.People.Cheater];

            function e(e, t, r, n, o) {
                s = n;
                var i = new StringConverter;
                if (u = i.transformString, c = new TypeValidator, n()) {
                    switch (a = e, l = t, r[d.Action]) {
                        case d.People.Action.Set:
                            D(r);
                            break;
                        case d.People.Action.SetOnce:
                            D(r, !0);
                            break;
                        case d.People.Action.Unset:
                            h(r, !0);
                            break;
                        case d.People.Action.Clear:
                            f.info(d.Log.ClearSuccessful, {})
                    }
                    o(r[d.Action], a, l)
                }
            }

            function v(e) {
                return u(e, {max: {value: 255, name: e, msg: d.Log.StringKeyNotLonger}})
            }

            e.prototype.getProperties = function () {
                return a
            }, e.prototype.getUpdateProperties = function () {
                return l
            };

            function m(e, t) {
                return c.checkString(e, t)
            }

            var h = function (e) {
                var t = e[d.People.Properties];
                t = o.isArray(t) ? t : [t], o.forEach(function (e) {
                    c.checkString(e, "Custom event", {
                        errors: {
                            1: d.Log.ParameterNameWrongTypeNull,
                            2: d.Log.ParameterNameWrongType
                        }
                    }) && (e = v(e), s() && (l[e] = null, a[e] = null, f.info(d.Log.UnsetPeopleSuccessful, {})))
                }, t)
            }, D = function (e, t) {
                var n = 1 < arguments.length && void 0 !== t && t, r = e[d.People.Properties];
                p(function (e, t) {
                    if (c.checkString(t, "Custom event", {
                        errors: {
                            1: d.Log.ParameterNameWrongTypeNull,
                            2: d.Log.ParameterNameWrongType
                        }
                    })) {
                        var r = (t = v(t)).toLowerCase();
                        -1 !== y.indexOf(r) && (t = r), _(t, e, n) && (o.isString(e) && (e = function (e, t) {
                            return u(e, {max: {value: 255, name: t}})
                        }(e, "Custom event")), l[t] = e, a[t] = e, f.info(d.Log.SetPeopleSuccessful))
                    }
                }, r)
            }, _ = function (e, t, r) {
                var n = !0;
                if (n && r && i(a[e]) && (n = !1), n) switch (e) {
                    case d.People.Name:
                        n = m(t, "Name");
                        break;
                    case d.People.Email:
                        n = m(t, "Email");
                        break;
                    case d.People.Photo:
                        n = m(t, "Photo");
                        break;
                    case d.People.Phone:
                        n = m(t, "Phone");
                        break;
                    case d.People.Age:
                        n = function (e) {
                            return c.checkInteger(e, "Age", 0, void 0, {isEqMin: !0})
                        }(t);
                        break;
                    case d.People.Cheater:
                        n = function (e) {
                            return c.checkBoolean(e, "Cheater")
                        }(t);
                        break;
                    case d.People.Gender:
                        n = function (e) {
                            var t = c.checkNull(e, "Gender");
                            return t && -1 === [0, 1, 2].indexOf(e) && (f.warning(d.Log.SetPeopleGenderWrong), t = !1), t
                        }(t);
                        break;
                    default:
                        var o = {
                            1: d.Log.ParameterValueWrongTypeNull,
                            2: d.Log.ParameterValueWrongType,
                            3: d.Log.ParameterValueWrongType
                        };
                        n = g(t) ? c.checkInteger(t, "Custom event", 0, void 0, {errors: o}) : c.checkString(t, "Custom event", {errors: o})
                }
                return n
            };
            return e
        }(), DeferredMessageManager = function () {
            var n = !0, o = {}, i = DTDHelpers, a = [];

            function e() {
            }

            return e.prototype.add = function (e, t) {
                switch (e) {
                    case DTDActionType.SetUserId:
                        o = i.mergeRecursive(o, t);
                        break;
                    case DTDActionType.GenerateEvent:
                        if (t[DTDEventKeys.Code] === DTDEventKeys.TrackingAvailability.Code) {
                            var r = t[DTDEventKeys.TrackingAvailability.TrackingAllowed];
                            i.isBoolean(r) && ((n = r) || (a = []), a.push({
                                command: DTDActionType.GenerateEvent,
                                parameters: t
                            }))
                        } else n && a.push({command: e, parameters: t})
                }
            }, e.prototype.clear = function () {
                a = [], o = {}
            }, e.prototype.get = function () {
                return a
            }, e.prototype.getBundle = function () {
                return o
            }, e
        }(), DTDEventModel = function () {
            "use strict";

            function e(e) {
                t = e
            }

            var t, r = DTDLogger.getInstance();
            return e.prototype.add = function (e) {
                !function (e) {
                    e[DTDEventKeys.Id] = DTDHelpers.generateUUID(), t.insertEvent(e), r.debug("ADD EVENT: ".concat(JSON.stringify(e)))
                }(e)
            }, e.prototype.get = function () {
                return t.getEvent()
            }, e.prototype.replace = function (e) {
                return t.replaceEvent(e)
            }, e
        }(), DTDAppModel = function () {
            "use strict";

            function e(e) {
                i = e, Object.defineProperty(this, "excludeVersion", {
                    get: function () {
                        return i.getApp("config.exclude.version")
                    }
                }), Object.defineProperty(this, "excludeEvents", {
                    get: function () {
                        var e = i.getApp("config.exclude.events");
                        if (i.getApp("config.exclude.all")) e = {}; else if (n.isNullOrEmpty(e)) return null;
                        return e
                    }
                })
            }

            var i, r = DTDKeys.App, n = DTDHelpers;
            DTDKeys.SdkConfig;
            return e.prototype.getDummyId = function () {
                var e = r.DummyId;
                return i.getApp(e) || i.saveApp(e, n.generateUUID()), i.getApp(e)
            }, e.prototype.updateConfig = function (e) {
                var t = (e && e.exclude ? e.exclude : {}).version || null,
                    r = e.exclude && e.exclude.events ? e.exclude.events : [], n = i.getApp("config.exclude.version"),
                    o = this.excludeVersion;
                n === t && (r = o), i.saveApp("config", _objectSpread({}, e, {exclude: _objectSpread({}, e.exclude, {events: r})}))
            }, e.prototype.insertTransactionIds = function (e) {
                var t = this.getTransactionIds();
                n.isNullOrEmpty(e) || (t.push(e), i.saveApp(r.TransactionIds, t))
            }, e.prototype.getTransactionIds = function () {
                return i.getApp(r.TransactionIds) || []
            }, e.prototype.getVersion = function () {
                return i.getApp(r.AppVersion)
            }, e.prototype.setVersion = function (e) {
                i.saveApp(r.AppVersion, e)
            }, e.prototype.getEventParamsCount = function () {
                return i.getApp("config.eventParamsCount") || 10
            }, e.prototype.getCountForRequest = function () {
                return i.getApp("config.countForRequest") || 20
            }, e.prototype.getSessionTimeout = function () {
                return i.getApp("config.sessionTimeout") || 6e5
            }, e.prototype.getAliveTimeout = function () {
                var e = i.getApp("config.aliveTimeout");
                return e = e || 12e4
            }, e.prototype.getUserCounting = function () {
                return i.getApp("config.userCounting") || !1
            }, e.prototype.getAbTestStartTimeout = function () {
                return i.getApp("config.abTestStartTimeout") || 3e4
            }, e
        }(), DTDDeviceModel = function () {
            var r, i = null, a = DTDHelpers, s = DTDKeys.Device;

            function e(e) {
                r = e
            }

            e.prototype.getPrimaryId = function () {
                return r.getDeviceFieldById(i, s.PrimaryId)
            }, e.prototype.getDeviceId = function () {
                return r.getDeviceFieldById(i, s.DeviceId)
            }, e.prototype.getPrevDeviceId = function () {
                return r.getDeviceFieldById(i, s.PrevDeviceId)
            }, e.prototype.getTrackingStatus = function () {
                var e = r.getDeviceFieldById(i, s.TrackingAvailability);
                return !!a.isNullOrEmpty(e) || e
            }, e.prototype.setTrackingAvailability = function (e) {
                u(_defineProperty({}, s.TrackingAvailability, e))
            };
            var u = function (e) {
                var t = r.getDeviceById(i);
                return t = _objectSpread({}, t, {}, e), r.saveDeviceById(i, t), t
            };
            return e.prototype.initialize = function (e, t, r) {
                var n = {};
                if (i = e, a.isNullOrEmpty(e)) i = a.generateUUID(), n[s.DeviceId] = t, n[s.PrimaryId] = i; else if (this.getDeviceId() !== t) {
                    var o = this.getDeviceId();
                    n[s.PrevDeviceId] = o, n[s.DeviceId] = t
                }
                n[s.IsDummy] = r, u(n)
            }, e
        }(), DTDUserModel = function () {
            "use strict";

            function e(e) {
                a = e
            }

            var a, s = null, u = DTDHelpers, c = DTDKeys.User, l = DTDEventKeys, t = DTDKeys;
            e.prototype.setBackendIds = function (e) {
                var t = {}, r = e.backendDeviceId, n = e.backendCrossPlatformId;
                u.isNullOrEmpty(r) || (t[c.BackendDeviceId] = r), u.isNullOrEmpty(n) || (t[c.BackendCrossPlatformId] = n), i(t)
            };
            var i = function (e) {
                var t = a.getUserById(s);
                return t = _objectSpread({}, t, {}, e), a.saveUserById(s, t), t
            };
            return e.prototype.initialize = function (e, t, r, n) {
                s = e, u.isNullOrEmpty(e) ? function (e, t, r) {
                    var n;
                    s = u.generateUUID();
                    var o = (_defineProperty(n = {}, c.PrimaryId, s), _defineProperty(n, c.RegistrationDate, u.getCurrentTimeMs()), _defineProperty(n, c.DeviceId, t), _defineProperty(n, c.UserId, e), _defineProperty(n, c.IsDummy, r), n);
                    i(o)
                }(t, r, n) : function (e, t) {
                    if (!t) {
                        var r = {}, n = a.getUserById(s)[c.UserId];
                        n !== e && (r[c.PrevUserId] = n, r[c.UserId] = e), r[c.IsDummy] = t, i(r)
                    }
                }(t, n)
            }, e.prototype.needIdentifiersRequest = function (e) {
                return u.isNullOrEmpty(this.backendDeviceId) || e && u.isNullOrEmpty(this.backendCrossPlatformId)
            }, e.prototype.insertTutorialStep = function (e) {
                var t = this.getTutorialStep();
                t.push(e), a.saveUserFieldById(s, c.TutorialSteps, t)
            }, e.prototype.getTutorialStep = function () {
                return a.getUserFieldById(s, c.TutorialSteps) || []
            }, e.prototype.getCurrentLevel = function () {
                return a.getUserFieldById(s, c.CurrentLevel) || 1
            }, e.prototype.setCurrentLevel = function (e) {
                return a.saveUserFieldById(s, c.CurrentLevel, e)
            }, e.prototype.getUserId = function () {
                return a.getUserFieldById(s, c.UserId)
            }, e.prototype.getPrimaryId = function () {
                return s
            }, e.prototype.getPrevUserId = function () {
                return a.getUserFieldById(s, c.PrevUserId)
            }, e.prototype.getBackendCrossPlatformId = function () {
                return a.getUserFieldById(s, c.BackendCrossPlatformId)
            }, e.prototype.getBackendDeviceId = function () {
                return a.getUserFieldById(s, c.BackendDeviceId)
            }, e.prototype.getDeviceId = function () {
                return a.getUserFieldById(s, c.DeviceId)
            }, e.prototype.getUserCard = function () {
                return a.getUserFieldById(s, c.Profile) || {}
            }, e.prototype.getUserCardDiff = function () {
                return a.getUserFieldById(s, c.UpdateProfile)
            }, e.prototype.clearUserCard = function () {
                var e = {};
                e[c.Profile] = null, e[c.UpdateProfile] = null, i(e)
            }, e.prototype.clearUserCardDiff = function () {
                var e = {};
                e[c.UpdateProfile] = null, i(e)
            }, e.prototype.setUserCard = function (e, t) {
                var r = {};
                u.isNullOrEmpty(e) || (r[c.Profile] = e), u.isNullOrEmpty(t) || (r[c.UpdateProfile] = t), i(r)
            }, e.prototype.isCheater = function () {
                return this.getUserCard()[t.People.Cheater] || !1
            }, e.prototype.update = function (e, t) {
                return i(_defineProperty({}, e, t))
            }, e.prototype.setCurrencyAccrual = function (e) {
                var t = e[l.CurrencyAccrual.CurrencySource], r = e[l.CurrencyAccrual.AccrualType],
                    n = e[l.CurrencyAccrual.CurrencyAmount], o = e[l.CurrencyAccrual.CurrencyName],
                    i = this.getCurrencyAccrual();
                r = DTDAccrualType[r], u.isNullOrEmpty(i[r]) && (i[r] = {}), u.isNullOrEmpty(i[r][t]) && (i[r][t] = {}), u.isNullOrEmpty(i[r][t][o]) && (i[r][t][o] = 0), i[r][t][o] += n, a.saveUserFieldById(s, c.CurrencyAccrual, i)
            }, e.prototype.getCurrencyAccrual = function () {
                return a.getUserFieldById(s, c.CurrencyAccrual) || {}
            }, e.prototype.clearCurrencyAccrual = function () {
                a.saveUserFieldById(s, c.CurrencyAccrual, {})
            }, e.prototype.isActiveReferralStatus = function () {
                var e = a.getUserFieldById(s, c.ReferralSatus);
                return u.isNullOrEmpty(e) ? 1 : e
            }, e.prototype.turnOffReferralStatus = function () {
                a.saveUserFieldById(s, c.ReferralSatus, 0)
            }, e.prototype.setIsUserPay = function () {
                a.saveUserFieldById(s, c.IsPayingUser, 1)
            }, e.prototype.isPayingUser = function () {
                return !!a.getUserFieldById(s, c.IsPayingUser)
            }, e.prototype.getSessionLength = function () {
                return a.getUserFieldById(s, c.SessionLength) || 0
            }, e.prototype.getSessionStarted = function () {
                return a.getUserFieldById(s, c.SessionStarted)
            }, e.prototype.getLastForeground = function () {
                return a.getUserFieldById(s, c.LastForeground)
            }, e.prototype.getUserCardChanges = function () {
                return a.getUserFieldById(s, c.UpdateProfile) || {}
            }, e.prototype.startSession = function () {
                var e = {}, t = u.getCurrentTimeMs();
                e[c.SessionStarted] = parseInt(t / 1e3), e[c.LastForeground] = t, e[c.SessionLength] = 0, i(e)
            }, e.prototype.incrementSession = function () {
                var e = {}, t = u.getCurrentTimeMs(), r = parseInt((t - this.getLastForeground()) / 1e3),
                    n = this.getSessionLength();
                e[c.LastForeground] = t, e[c.SessionLength] = n + r, i(e)
            }, e.prototype.resumeSession = function () {
                var e = {}, t = u.getCurrentTimeMs();
                e[c.LastForeground] = t, i(e)
            }, e.prototype.clearSessionLength = function () {
                var e = {};
                e[c.SessionLength] = 0, i(e)
            }, e.prototype.setExpiriments = function (e) {
                var t = _defineProperty({}, c.UserExperiments, e);
                i(t)
            }, e.prototype.removeExpiriments = function () {
                var e = a.getUserById(s);
                delete e[c.UserExperiments], i(e)
            }, e.prototype.getExpiriments = function () {
                return a.getUserFieldById(s, c.UserExperiments) || {}
            }, e.prototype.setRemoteConfig = function (e) {
                var t = _defineProperty({}, c.UserRemoteConfig, e);
                i(t)
            }, e.prototype.getRemoteConfig = function () {
                return a.getUserFieldById(s, c.UserRemoteConfig) || {}
            }, e
        }();
    DTDVerificationCondition = function () {
        var o = DTDHelpers;

        function e() {
        }

        function r(e, r) {
            return o.isObject(e) ? !o.findObj(function (e, t) {
                switch (t) {
                    case"IN":
                    case"NOT IN":
                        return !function (e, t, r) {
                            if (!o.isArray(t)) return !1;
                            if (!o.isString(r) && !o.isNumber(r)) return !1;
                            var n = -1 < t.indexOf(r);
                            return "NOT IN" === e ? !n : n
                        }(t, e, r);
                    case"==":
                    case"!=":
                        return !function (e, t, r) {
                            var n = o.isEqual(t, r);
                            return "!=" === e ? !n : n
                        }(t, e, r);
                    case">":
                    case"<":
                    case">=":
                    case"<=":
                        return !function (e, t, r) {
                            if (_typeof(t) === _typeof(r)) if (o.isNumber(r)) switch (e) {
                                case">=":
                                    return t <= r;
                                case">":
                                    return t < r;
                                case"<":
                                    return r < t;
                                case"<=":
                                    return r <= t
                            } else if (o.isString(r)) {
                                var n = r.localeCompare(t);
                                switch (e) {
                                    case">=":
                                    case">":
                                        return 0 <= n;
                                    case"<":
                                        return n < 0;
                                    case"<=":
                                        return n <= 0
                                }
                            }
                            return !1
                        }(t, e, r);
                    default:
                        return !1
                }
            }, e) : o.isEqual(e, r)
        }

        return e.prototype.checkConditions = function (e, t) {
            return r(e, t)
        }, e
    }();
    var RemoteConfigUser = function () {
        "use strict";

        function e() {
        }

        var t, r = DTDHelpers, n = DTDKeys.User, o = DTDKeys.UserExperiments, i = r.isNullOrEmpty;
        e.prototype.setUser = function (e) {
            t = e
        };

        function a() {
            return t[n.UserExperiments] || {}
        }

        e.prototype.getExpiriments = function () {
            return t[n.UserExperiments] || {}
        };

        function s() {
            return a()[o.Configs]
        }

        function u() {
            return a()[o.Status]
        }

        e.prototype.thereAreNoNonfirmedExperiments = function () {
            return 2 === u() && !i(s()) || 1 === u() && !i(s())
        }, e.prototype.getTimeWait = function () {
            return (new Date).getTime() - (a()[o.Time] || 0)
        }, e.prototype.getTimeMin = function () {
            return (new Date).getTime() - 5 * (a()[o.Time] || 0)
        }, e.prototype.getTimeStart = function () {
            return a()[o.Time] || 0
        }, e.prototype.inExperiments = function () {
            return 3 === u() && !i(s()) && !c()
        };
        var c = function () {
            return a()[o.IsTesting] || !1
        };
        return e.prototype.get = function () {
            return s()
        }, e
    }();
    RemoteConfig = function () {
        var r, n;
        return function (e, t) {
            r = e, n = t, Object.defineProperty(this, "stringValue", {
                get: function () {
                    return String(r)
                }
            }), Object.defineProperty(this, "doubleValue", {
                get: function () {
                    return Number(r)
                }
            }), Object.defineProperty(this, "longValue", {
                get: function () {
                    var e = Number(r);
                    return e ? Math.floor(e) : e
                }
            }), Object.defineProperty(this, "booleanValue", {
                get: function () {
                    return !!r
                }
            }), Object.defineProperty(this, "source", {
                get: function () {
                    return n
                }
            })
        }
    }();
    var RemoteConfigContainer = function () {
        var t, i, a, s, u, c = DTDHelpers, l = DTDEventKeys, d = DTDKeys, f = {}, p = null, g = null,
            o = DTDLogger.getInstance(), y = c.forEachObj, v = c.findObj, m = c.forEach, h = c.isNullOrEmpty,
            D = d.RemoteConfig, _ = D.ExperimentsSettings, e = D.UserProperties, P = d.UserRemoteConfig,
            T = d.Identifiers, C = d.UserExperiments;

        function r(e, t, r, n, o) {
            p = e, g = t, i = o, u = r, s = n
        }

        function S() {
            return p.getExpiriments()[C.Configs]
        }

        function n() {
            return p.getExpiriments()[C.Status]
        }

        function I() {
            return 1 === n() && !h(S())
        }

        function b() {
            var e = V(), r = {};
            return c.forEach(function (e) {
                var t = e[_.Id];
                r[t] = e
            }, e), r
        }

        function w(e, t) {
            return !!t[e] && (t[e][_.CompletionDate] > c.getCurrentTimeMs() || t[e][_.IsTesting])
        }

        function k(e, t, r) {
            if (!r || e !== r[l.Code]) return !1;
            var n = new DTDVerificationCondition;
            return !v(function (e, t) {
                return !(t !== l.People.Parameters ? n.checkConditions(e, r[t]) : R(e, r[l.People.Parameters]))
            }, t)
        }

        function E() {
            s.removeSchedulerTask("clearAbTest"), p.removeExpiriments()
        }

        function A(e) {
            !function () {
                var e,
                    t = new RequestStructure((_defineProperty(e = {}, T.DeviceId, g.getDeviceId()), _defineProperty(e, T.PreviousDeviceId, g.getPrevDeviceId()), _defineProperty(e, T.CrossPlatformId, p.getUserId()), _defineProperty(e, T.PreviousCrossPlatformId, p.getPrevUserId()), e));
                i.fetch(a, t, function (e) {
                    U(P.Settings, e), o.debug("Set remote config" + JSON.stringify(p.getRemoteConfig()[P.Settings])), O(), x() ? L() : N() || z()
                })
            }(), t = e[D.Callback]
        }

        var U = function (e, t) {
            var r = p.getRemoteConfig();
            r[e] = t, p.setRemoteConfig(r)
        }, N = function () {
            return 3 === n() && !h(S())
        }, x = function () {
            return 2 === n() && !h(S())
        }, O = function () {
            var r = b(), e = S(), n = [];
            if (!h(e)) {
                var t = N();
                if (m(function (e, t) {
                    w(e.id, r) && n.push(e)
                }, e), 0 === n.length) p.removeExpiriments(); else if (n.length !== e.length) {
                    var o = p.getExpiriments();
                    o[C.Configs] = n, p.setExpiriments(o)
                }
                t !== N() && L()
            }
        }, L = function () {
            t && t()
        }, R = function (e, n) {
            var o = new DTDVerificationCondition;
            return !v(function (e, t) {
                if (t !== l.People.Parameters) var r = o.checkConditions(e, n[t]);
                return !r
            }, e)
        }, V = function () {
            return (p.getRemoteConfig()[P.Settings] || {})[_.Key] || {}
        }, B = function () {
            return (p.getRemoteConfig()[P.Settings] || {})[e.Key] || {}
        }, z = function (n) {
            var e = V(), o = [];
            if (N() || x() || (c.forEach(function (e, t) {
                var r = null;
                !0 !== e[C.IsTesting] && (r = v(function (e) {
                    var t = e.code;
                    e.code, e.code, e.code, t = e.code;
                    return c.isNullOrEmpty(e.type) ? t === l.People.Code ? !function (e, t) {
                        return !(!t || c.isNullOrEmpty(e[l.People.Parameters])) && R(e[l.People.Parameters], t)
                    }(e, p.getUserCard()) : t === l.DeviceInfo.Code ? !k(t, e, f) : !k(t, e, n) : "backend" === e.type ? !function (e) {
                        var n = new DTDVerificationCondition, o = B();
                        return !v(function (e, t) {
                            var r = !0;
                            switch (t) {
                                case D.Backend.Country:
                                    r = n.checkConditions(e, o[D.Backend.Country]);
                                    break;
                                case D.Backend.Level:
                                    r = n.checkConditions(e, p.getCurrentLevel());
                                    break;
                                case D.Backend.PayingUser:
                                    r = n.checkConditions(e, p.isPayingUser());
                                    break;
                                case"type":
                                    r = !0;
                                    break;
                                default:
                                    r = !1
                            }
                            return !r
                        }, e)
                    }(e) : void 0
                }, e[_.Conditions] || [])), h(r) && o.push({id: e[_.Id]})
            }, e), !(0 < o.length) || N() || x())) return !1;
            var t = p.getExpiriments();
            return t[C.Configs] = o, t[C.Status] = 1, t[C.Time] = (new Date).getTime(), p.setExpiriments(t), s.addSchedulerTask("clearAbTest", u.getAbTestStartTimeout(), E), function () {
                var e,
                    t = new RequestStructure((_defineProperty(e = {}, T.DeviceId, g.getDeviceId()), _defineProperty(e, T.PreviousDeviceId, g.getPrevDeviceId()), _defineProperty(e, T.CrossPlatformId, p.getUserId()), _defineProperty(e, T.PreviousCrossPlatformId, p.getPrevUserId()), e));
                if (I()) {
                    var r = S();
                    if (r) {
                        var n = [], u = b();
                        m(function (e, t) {
                            w(e.id, u) && n.push(e.id)
                        }, r), t.addBody("suitableExperiments", n), i.offer(a, t, function (e) {
                            if (e.involvedExperiments) {
                                var i = [], a = [], s = !1;
                                if (m(function (e, t) {
                                    var r = e[_.Id], n = e[_.Group];
                                    if (!h(u[r])) {
                                        var o = {};
                                        s = s || u[r][_.IsTesting], c.forEach(function (e) {
                                            var t = e.key, r = e.values;
                                            o[t] = r[n]
                                        }, u[r][_.Parameters]), a.push({id: e.id, group: e.group}), i.push(o)
                                    }
                                }, e.involvedExperiments), h(a)) p.removeExpiriments(); else {
                                    var t = p.getExpiriments();
                                    t[C.Configs] = a, t[C.Status] = 2, t[C.Parameters] = i, t[C.IsTesting] = s ? 1 : 0, p.setExpiriments(t), L()
                                }
                            }
                        })
                    }
                }
            }(), !0
        };
        return r.prototype.inExperiments = function () {
            return N() && !p.getExpiriments()[C.IsTesting]
        }, r.prototype.start = function () {
            if (I() || x()) {
                var e = (new Date).getTime() - (p.getExpiriments()[C.Time] || 0);
                e >= u.getAbTestStartTimeout() ? E() : s.addSchedulerTask("clearAbTest", u.getAbTestStartTimeout() - e, E)
            }
        }, r.prototype.checkingCondition = function (e) {
            I() || z(e)
        }, r.prototype.get = function () {
            return S()
        }, r.prototype.executeCommand = function (e) {
            switch (e[d.Action]) {
                case D.Action.Fetch:
                    A(e);
                    break;
                case D.Action.Activate:
                    !function () {
                        O();
                        var e;
                        x() ? (e = p.getExpiriments(), s.removeSchedulerTask("clearAbTest"), e[C.Status] = 3, p.setExpiriments(e), o.info("Start experiment " + JSON.stringify(p.getExpiriments()))) : N() && o.info("Experiment already started")
                    }();
                    break;
                case D.Action.SetDefaultsConfig:
                    !function (e, t) {
                        var r = t[d.Properties], n = p.getRemoteConfig()[e] || {};
                        y(function (e, t) {
                            c.isNull(e) ? delete n[t] : n[t] = e
                        }, r), U(e, n), o.debug("Set default config" + JSON.stringify(n))
                    }(P.DefaultsConfig, e)
            }
        }, r.prototype.getterData = function (e) {
            switch (e[d.Action]) {
                case D.Action.GetABTest:
                    var t = [];
                    return N() && (t = S()), t;
                case D.Action.GetDefaultsConfig:
                    return p.getRemoteConfig()[P.DefaultsConfig] || {};
                case D.Action.GetRemoteConfig:
                    var r = {};
                    if (N()) r = p.getExpiriments()[C.Parameters];
                    return r;
                case D.Action.GetConfigKey:
                    return function (e, t) {
                        var r, n = 1 < arguments.length && void 0 !== t ? t : DTDRemoteConfigSource.Ending,
                            o = e[d.RemoteConfig.ConfigKey], i = p.getExpiriments()[C.Parameters] || [],
                            a = p.getRemoteConfig()[P.DefaultsConfig] || {};
                        return h(i) || m(function (e) {
                            h(e[o]) || (r = e[o])
                        }, i), h(r) && !c.isNullOrEmpty(a[o]) && (r = a[o]), new RemoteConfig(r, n)
                    }(e)
            }
        }, r.prototype.setDeviceInfo = function (e) {
            f = e
        }, r.prototype.setApplicationId = function (e) {
            a = e
        }, r
    }(), DTDProgression = function () {
        function e() {
            s = [], a = {}
        }

        var a = {}, s = [], u = DTDLogger.getInstance(), n = DTDEventKeys, c = DTDKeys, l = DTDHelpers,
            d = n.ProgressionEvent, o = l.forEachObj;
        return e.prototype.start = function (e, t) {
            return this.clear(), void 0 === a[e] && s.push(e), a[e] = {
                startTime: l.getCurrentTimeMs(),
                package: t
            }, u.info(c.Log.ProgressionStart, {name: e}), !0
        }, e.prototype.finish = function (e, t) {
            if (void 0 === a[e]) return u.info(c.Log.FinishFailStart, {name: e}), !1;
            var r = a[e].package, n = a[e].startTime;
            if (r = l.mergeRecursive(r, t), u.info(c.Log.ProgressionFinish, {name: e}), l.isNullOrEmpty(r[d.Success]) && (r[d.Success] = !1), l.isNullOrEmpty(r[d.Duration])) {
                var o = parseInt((l.getCurrentTimeMs() - n) / 1e3);
                r[d.Duration] = o
            }
            a[e].package = r;
            var i = s.indexOf(e);
            return -1 !== i && s.splice(i, 1), !0
        }, e.prototype.inProgress = function () {
            return s
        }, e.prototype.clear = function () {
            a = {}, s = []
        }, e.prototype.build = function (e) {
            var t = a[e];
            delete a[e];
            var r = _defineProperty({}, n.Parameters, {});
            return void 0 !== t && o(function (e, t) {
                -1 === [d.Difficulty, d.Source, d.Success, d.Duration].indexOf(t) ? r[t] = e : r[n.Parameters][t] = e
            }, t.package), r
        }, e
    }(), DTDAnalyticsState = function () {
        "use strict";

        function e(e, t, r) {
            g = e, s = new DTDStorage, u = new DTDAppModel(s), c = new DTDUserModel(s), l = new DTDDeviceModel(s), i = new DTDEventModel(s), d = new DTDEventValidator(u, l, c), f = new DTDEventConverter(u), p = new DTDProgression, y = new RemoteConfigContainer(c, l, u, t, r)
        }

        function a(e, t) {
            var r;
            return u.getUserCounting() ? _defineProperty({}, S.UserId, t) : (_defineProperty(r = {}, S.DeviceId, e), _defineProperty(r, S.UserId, t), r)
        }

        function o(e) {
            try {
                var t = m.isNullOrEmpty(e) ? 1 : 0, r = l.getPrimaryId();
                e = t ? l.getDeviceId() : e;
                var n, o, i = null;
                if (n = function (e, t) {
                    var r, n = {};
                    u.getUserCounting() ? (_defineProperty(r = {}, S.DeviceId, e), _defineProperty(r, S.UserId, t), n = r) : n = _defineProperty({}, S.DeviceId, e);
                    return n
                }(r, e), i = s.findPrimaryUserId(n), m.isNullOrEmpty(i)) i = s.findPrimaryUserId((_defineProperty(o = {}, S.IsDummy, 1), _defineProperty(o, S.DeviceId, r), o));
                return c.initialize(i, e, r, t), !0
            } catch (e) {
                return v.error(e), !1
            }
        }

        function t() {
            if (0 < c.getSessionLength()) {
                var e,
                    t = (_defineProperty(e = {}, h.Code, h.UserEngagement.Code), _defineProperty(e, h.UserEngagement.Length, c.getSessionLength()), e);
                c.clearSessionLength(), I(t)
            }
        }

        function r(e) {
            var t = (e = f.transformData(DTDActionType.SetUserId, e))[D.DeviceId], r = e[D.PrevDeviceId],
                n = e[D.UserId];
            return function (e, t) {
                var r = m.isNullOrEmpty(e) ? 1 : 0, n = null;
                r && (e = u.getDummyId()), n = s.findPrimaryDeviceId(_defineProperty({}, C.DeviceId, e)), m.isNullOrEmpty(n) && (m.isNullOrEmpty(t) || (n = s.findPrimaryDeviceId(_defineProperty({}, C.DeviceId, t))), m.isNullOrEmpty(n) && !r && (u.getUserCounting() || (n = s.findPrimaryDeviceId(_defineProperty({}, C.IsDummy, 1))))), l.initialize(n, e, r)
            }(t, r), o(n), m.isNullOrEmpty(e[D.Level]) || b(_defineProperty({}, h.Level, e[D.level])), m.isNullOrEmpty(e[D.TrackingAvailability]) || l.setTrackingAvailability(e[D.TrackingAvailability]), !0
        }

        function n() {
            if (!m.isNullOrEmpty(c.getCurrencyAccrual())) {
                var e = _objectSpread(_defineProperty({}, h.Code, h.CurrencyAccrual.Code), c.getCurrencyAccrual());
                if (I(e)) return c.clearCurrencyAccrual(), !0
            }
            return !1
        }

        var s, u, c, i, l, d, f, p, g, y, v = DTDLogger.getInstance(), m = DTDHelpers, h = DTDEventKeys, D = DTDKeys,
            _ = DTDApplicationInfo.getInstance(), P = DTDSystemInfo.getInstance(), T = DTDKeys.Log, C = DTDKeys.Device,
            S = DTDKeys.User, I = function (e) {
                var t = (e = f.transformEvent(e))[h.Code];
                if (d.canGenerateEvent(e)) {
                    !function (e) {
                        var t = e[h.Code];
                        switch (t) {
                            case h.LevelUp.Code:
                                n();
                                break;
                            case h.TrackingAvailability.Code:
                                l.setTrackingAvailability(e[h.TrackingAvailability.TrackingAllowed])
                        }
                        -1 < [h.Tutorial.Code, h.CustomEvent.Code, h.VirtualCurrencyPayment.Code, h.LevelUp.Code, h.ProgressionEvent.Code, h.RealCurrencyPayment.Code].indexOf(t) && y.checkingCondition(e)
                    }(e);
                    var r = function (e) {
                        var t;
                        e.timestamp = m.getCurrentTimeMs();
                        var r = e[h.Code],
                            n = (_defineProperty(t = {}, h.UserId, c.getPrimaryId()), _defineProperty(t, h.Language, P.getLanguage()), _defineProperty(t, h.AppVersion, u.getVersion()), _defineProperty(t, h.SdkVersion, _.getSdkVersion()), _defineProperty(t, h.SdkCodeVersion, _.getSdkCodeVersion()), t);
                        return n = m.filterObj(function (e, t) {
                            return !m.isNullOrEmpty(e)
                        }, n), m.isNullOrEmpty(p.inProgress()) || -1 !== [h.DeviceInfo.Code, h.People.Code, h.Referrer.Code, h.ProgressionEvent.Code, h.Alive.Code].indexOf(r) || (e.inProgress = p.inProgress()), -1 === [h.DeviceInfo.Code, h.TrackingAvailability.Code, h.LevelUp.Code, h.Alive.Code].indexOf(r) && (e.level = c.getCurrentLevel()), -1 === [h.DeviceInfo.Code, h.TrackingAvailability.Code, h.SessionStart.Code, h.Alive.Code].indexOf(r) && (e[h.SessionId] = c.getSessionStarted()), y.inExperiments() && (e[h.InExperiments] = y.get()), n[h.Events] = e, n
                    }(e);
                    return i.add(r), v.info(T.Added, {event: t}), function (e) {
                        switch (e[h.Code]) {
                            case h.Tutorial.Code:
                                c.insertTutorialStep(e[h.Tutorial.Step]);
                                break;
                            case h.RealCurrencyPayment.Code:
                                u.insertTransactionIds(e[h.RealCurrencyPayment.OrderId]), c.setIsUserPay(!0), g();
                                break;
                            case h.LevelUp.Code:
                                b(_defineProperty({}, h.Level, e[h.LevelUp.Level]));
                                break;
                            case h.Referrer.Code:
                                c.turnOffReferralStatus();
                                break;
                            case h.TrackingAvailability.Code:
                                g()
                        }
                    }(r[h.Events]), !0
                }
                return !1
            }, b = function (e) {
                d.canSetData(DTDActionType.SetCurrentLevel, e) && (c.setCurrentLevel(e[h.Level]), v.info(T.SetLevelSuccessful, {
                    level: c.getCurrentLevel(),
                    user: c.getUserId()
                }))
            };
        e.prototype.registerUser = function (e) {
            var t = (e = f.transformData(DTDActionType.SetUserId, e))[DTDKeys.UserId];
            return !!(t !== c.getUserId() || m.isNullOrEmpty(t) && c.getUserId() === u.getDummyId()) && o(t)
        }, e.prototype.progressionEvent = function (e) {
            var t = e[D.State],
                r = (e = f.transformData(DTDActionType.SetProgressionEvent, e))[h.ProgressionEvent.Name];
            if (d.canSetData(DTDActionType.SetProgressionEvent, e)) switch (t) {
                case DTDProgressionState.Start:
                    p.start(r, e);
                    break;
                case DTDProgressionState.Finish:
                    p.finish(r, e) && I(p.build(r))
            }
        }, e.prototype.generateEvent = function (e) {
            return I(e)
        }, e.prototype.setCurrencyAccrual = function (e) {
            d.canSetData(DTDActionType.SetCurrencyAccrual, e) && (e = f.transformData(DTDActionType.SetCurrencyAccrual, e), c.setCurrencyAccrual(e), v.info("Set currency accrual: {data}", {data: JSON.stringify(c.getCurrencyAccrual())}))
        }, e.prototype.setCurrentLevel = function (e) {
            b(e)
        }, e.prototype.setAppVersion = function (e) {
            d.canSetData(DTDActionType.SetAppVersion, e) && (u.setVersion(e[h.AppVersion]), v.info(T.SetAppSuccessful, {version: u.getVersion()}))
        }, e.prototype.replaceUserId = function (e) {
            if (e = f.transformData(DTDActionType.ReplaceUserId, e), d.canSetData(DTDActionType.ReplaceUserId, e)) {
                var t = e[h.PrevUserId], r = e[h.UserId], n = a(l.getPrimaryId(), r);
                if (m.isNullOrEmpty(s.findPrimaryUserId(n))) {
                    n = a(l.getPrimaryId(), t);
                    var o = s.findPrimaryUserId(n);
                    if (!m.isNullOrEmpty(o)) {
                        var i = s.getUserById(o);
                        i[S.PrevUserId] = t, i[S.UserId] = r, i[S.IsDummy] = 0, s.saveUserById(o, i), c.getPrimaryId() === o && w(), v.info(T.RenameUserSuccessful, {
                            oldName: t,
                            newName: r
                        })
                    }
                } else v.info(T.RenameUserNewExist, {oldName: t, newName: r})
            }
        }, e.prototype.loadStorage = function () {
            w()
        }, e.prototype.initialize = function (e, t) {
            try {
                return s.initialize(e), y.setApplicationId(e), r(t)
            } catch (e) {
                return v.error(e), !1
            }
        };
        var w = function () {
            s.load()
        };
        e.prototype.generateUserEngagement = function () {
            return t()
        };
        e.prototype.getApp = function () {
            return u
        }, e.prototype.getUser = function () {
            return c
        }, e.prototype.getDevice = function () {
            return l
        }, e.prototype.getEvent = function () {
            return i
        }, e.prototype.getStorage = function () {
            return s
        };
        e.prototype.clearSession = function () {
            c.clearSession()
        }, e.prototype.startUserLifeCycle = function () {
            p.clear(), k(), function () {
                var e,
                    t = (_defineProperty(e = {}, h.Code, h.DeviceInfo.Code), _defineProperty(e, h.DeviceInfo.OsVersion, P.getOsVersion()), _defineProperty(e, h.DeviceInfo.DisplayResolution, P.getDisplayResolution()), _defineProperty(e, h.DeviceInfo.Os, P.getOsName()), _defineProperty(e, h.DeviceInfo.UUID, l.getDeviceId()), _defineProperty(e, h.DeviceInfo.UserAgent, P.getUserAgent()), _defineProperty(e, h.DeviceInfo.TimeZoneOffset, P.getTimeZoneOffset()), e);
                t = f.transformEvent(t), y.setDeviceInfo(_objectSpread({}, t, {language: P.getLanguage()})), I(t)
            }(), function () {
                if (c.isActiveReferralStatus()) try {
                    var r, e = {
                        utm_source: h.Referrer.Source,
                        utm_medium: h.Referrer.Medium,
                        utm_content: h.Referrer.Content,
                        utm_campaign: h.Referrer.Campaign,
                        utm_term: h.Referrer.Term
                    }, n = !0, o = {}, i = window.document.URL;
                    m.forEachObj(function (e, t) {
                        r = m.parseUrlParams(i, t), m.isNullOrEmpty(r) || (o[e] = r, n = !1)
                    }, e), n || (o[h.Code] = h.Referrer.Code, I(o))
                } catch (e) {
                }
            }(), y.start(), y.checkingCondition()
        }, e.prototype.resumeUserLifeCycle = function () {
            k(), y.start(), y.checkingCondition()
        };
        var k = function () {
            !function () {
                var e = m.getCurrentTimeMs();
                return c.getSessionStarted() && e - c.getLastForeground() < u.getSessionTimeout()
            }() ? 0 < c.getSessionLength() ? t() : (c.startSession(), v.debug("Start new session {sessionId} for user {user}", {
                sessionId: c.getSessionStarted(),
                user: c.getUserId()
            }), function () {
                var e = _defineProperty({}, h.Code, h.SessionStart.Code);
                I(e)
            }()) : (c.resumeSession(), v.debug("Resume session {sessionId} for user {user}", {
                sessionId: c.getSessionStarted(),
                user: c.getUserId()
            }))
        };
        e.prototype.incrementSession = function () {
            c.incrementSession()
        }, e.prototype.setUserCard = function (e) {
            var i = this;
            new DTDPeopleMerger(c.getUserCard(), c.getUserCardChanges(), e, d.canSetData, function (e, t, r) {
                if (e === D.People.Action.Clear) {
                    var n,
                        o = (_defineProperty(n = {}, h.Code, h.People.Code), _defineProperty(n, h.Parameters, null), n);
                    c.clearUserCard(null, null), I(o)
                } else c.setUserCard(t, r), i.checkExperiments()
            })
        }, e.prototype.getUserCard = function (e) {
            var t = e[D.Action];
            return c.getUserCard()[t]
        }, e.prototype.checkExperiments = function () {
            y.checkingCondition()
        };

        function E() {
            var e = c.getUserCardDiff();
            if (m.isNullOrEmpty(e)) return null;
            var t, r = (_defineProperty(t = {}, h.Code, h.People.Code), _defineProperty(t, h.Parameters, e), t);
            return c.clearUserCardDiff(), I(r)
        }

        return e.prototype.canSendEvent = function () {
            return E(), n(), 0 < i.get().length
        }, e.prototype.needSendEvent = function () {
            var e = c.getUserCardChanges(), t = i.get().length, r = u.getCountForRequest();
            return m.isNullOrEmpty(e) || t++, r <= t && (E(), !0)
        }, e.prototype.updateConfig = function (e) {
            u.updateConfig(e)
        }, e.prototype.executeRemoteConfig = function (e) {
            y.executeCommand(e)
        }, e.prototype.getDataRemoteConfig = function (e) {
            return y.getterData(e)
        }, e
    }(), DTDTimeManager = function () {
        var t, r, n = {}, o = {}, i = DTDHelpers;

        function e() {
            t = null, r = new DTDTimeManagerQueue
        }

        function a() {
            return !r.isNullOrEmpty() && r.first().deadline <= parseInt(i.getCurrentTimeMs() / 1e3) ? r.unshift() : null
        }

        function s() {
            clearTimeout(t);
            try {
                for (var e = a(); e;) void 0 !== n[e.id] && 1 == n[e.id].ignore || e.run(), e = a();
                t = setTimeout(s, 1e3)
            } catch (e) {
            }
        }

        e.prototype.start = function () {
            t = setTimeout(s, 0)
        }, e.prototype.stop = function () {
            clearTimeout(t), r.clear(), o = {}, n = {}
        };

        function u(e) {
            n[e.id] = e, r.addTask(e.deadline, e)
        }

        e.prototype.performTimedTask = function (e) {
            u(e)
        }, e.prototype.ignoreTimer = function (e) {
            i.isNullOrEmpty(n[e]) || (n[e].ignore = !0)
        };

        function c(e) {
            if (void 0 !== e) {
                var t = l(e.id), r = new DTDTimeManagerTask(t, e.interval, e.id);
                u(r)
            }
        }

        var l = function (e) {
            return function () {
                n[e].ignore ? delete n[task.id] : (o[e].callback(), c(o[e]))
            }
        };
        return e.prototype.addSchedulerTask = function (e, t, r) {
            o[e] = {id: e, interval: t, callback: r}, c(o[e])
        }, e.prototype.hasSchedulerTask = function (e) {
            return void 0 !== o[e]
        }, e.prototype.removeSchedulerTask = function (e) {
            void 0 !== o[e] && delete o[e], void 0 !== n[e] && (n[e].ignore = !0)
        }, e
    }(), DTDTimeManagerQueue = function () {
        var r, n;

        function e() {
            r = {}, n = []
        }

        return e.prototype.addTask = function (e, t) {
            -1 === n.indexOf(e) && (function (e) {
                n.push(e), n.sort(function (e, t) {
                    return parseFloat(e) - parseFloat(t)
                })
            }(e), r[e] = []), r[e].push(t)
        }, e.prototype.first = function () {
            if (!this.isNullOrEmpty()) return r[n[0]][0]
        }, e.prototype.isNullOrEmpty = function () {
            return 0 === n.length
        }, e.prototype.unshift = function () {
            if (this.isNullOrEmpty()) return !1;
            var e = n[0], t = r[e].shift();
            return 0 === r[e].length && (n.shift(), delete r[e]), t
        }, e.prototype.clear = function () {
            r = {}, n = []
        }, e
    }(), DTDTimeManagerTask = function () {
        function e(e, t, r) {
            var n = DTDHelpers.getCurrentTimeMs();
            n += t, this.deadline = parseInt(n / 1e3), this.callback = e, this.id = r || this.deadline, this.async = !1, this.running = !1
        }

        return e.prototype.run = function () {
            this.callback()
        }, e
    }(), DTDWorkEventListeners = function () {
        var o, i, a, s;

        function e(e, t, r, n) {
            o = e, i = t, a = r, s = n
        }

        function t(e, t, r) {
            e.addEventListener ? e.addEventListener(t, r, !1) : e.attachEvent("on" + t, r)
        }

        function r(e, t, r) {
            e.removeEventListener ? e.removeEventListener(t, r, !1) : e.detachEvent("on" + t, r)
        }

        function n() {
            return window.document.fullscreenElement || window.document.mozFullscreenElement || window.document.webkitFullscreenElement
        }

        function u() {
            t(window, "blur", o), t(window, "focus", i)
        }

        function c() {
            r(window, "blur", o), r(window, "focus", i)
        }

        function l(e) {
            e ? (c(), a()) : (u(), s())
        }

        return e.prototype.start = function () {
            t(window.document, "fullscreenchange", function () {
                l(n())
            }), t(window.document, "mozfullscreenchange", function () {
                l(n())
            }), t(window.document, "webkitfullscreenchange", function () {
                l(n())
            }), t(window.document, "msfullscreenchange", function () {
                l(n())
            }), u()
        }, e.prototype.stop = function () {
            c(), r(window.document, "fullscreenchange", function () {
                l(n())
            }), r(window.document, "mozfullscreenchange", function () {
                l(n())
            }), r(window.document, "webkitfullscreenchange", function () {
                l(n())
            }), r(window.document, "msfullscreenchange", function () {
                l(n())
            })
        }, e
    }(), DTDClient = function () {
        "use strict";

        function e() {
            c = new DTDTimeManager, f = new DTDRequestManager;
            var e = new RemoteConfigRequest(c, f);
            a = new DTDAnalyticsState(E, c, e), s = new DTDConfigManager(c, f), u = new DTDRequestCoordinator(a, c, f), d = new DTDWorkEventListeners(D, _, P, T)
        }

        function t() {
            a.incrementSession()
        }

        function r() {
            a.generateUserEngagement();
            var e = _defineProperty({}, m.Code, m.Alive.Code);
            a.generateEvent(e), E()
        }

        function n() {
            C(), c.addSchedulerTask("session", 5e3, t), c.addSchedulerTask("aliveTime", a.getApp().getAliveTimeout(), r)
        }

        function o(e) {
            var t = 0 < arguments.length && void 0 !== e ? e : [];
            h.forEach(function (e) {
                w(e.command, e.parameters, !0)
            }, t), a.getDevice().getTrackingStatus() && (a.startUserLifeCycle(), n(), I()), E()
        }

        function i() {
            var e, t = a.getApp(), r = a.getUser(), n = a.getDevice(),
                o = new RequestStructure((_defineProperty(e = {}, v.Identifiers.DeviceId, n.getDeviceId()), _defineProperty(e, v.Identifiers.PreviousDeviceId, n.getPrevDeviceId()), _defineProperty(e, v.Identifiers.CrossPlatformId, r.getUserId()), _defineProperty(e, v.Identifiers.PreviousCrossPlatformId, r.getPrevUserId()), _defineProperty(e, v.Identifiers.BackendDeviceId, r.getBackendDeviceId()), _defineProperty(e, v.Identifiers.BackendCrossPlatformId, r.getBackendCrossPlatformId()), e));
            o.addBody(m.SdkVersion, y.getSdkVersion()), o.addBody(m.AppVersion, t.appVersion), o.addBody(m.ExcludeVersion, t.excludeVersion), o.addBody(m.SdkCodeVersion, y.getSdkCodeVersion()), o.addToUrl("appId", l), o.setUrl(y.getConfigUrl()), s.fetch(o, function (e) {
                t.updateConfig(e)
            })
        }

        var a, s, u, c, l, d, f, p = !1, g = DTDLogger.getInstance(), y = DTDApplicationInfo.getInstance(), v = DTDKeys,
            m = DTDEventKeys, h = DTDHelpers, D = function () {
                c.stop(), a.generateUserEngagement(), C(), g.debug("Event onBlur")
            }, _ = function () {
                a.loadStorage(), c.start(), S(), g.debug("Event onFocus")
            }, P = function () {
                a.loadStorage(), c.start(), g.debug("Event full screen")
            }, T = function () {
                g.debug("Event normal screen")
            }, C = function () {
                c.removeSchedulerTask("session"), c.removeSchedulerTask("aliveTime")
            }, S = function () {
                a.getDevice().getTrackingStatus() && (a.resumeUserLifeCycle(), n(), I()), E()
            }, I = function () {
            };
        e.prototype.canStarted = function () {
            var e = window.navigator.userAgent;
            return !h.isBot(e)
        };

        function b(e, t, r) {
            l = e[v.ApplicationKey], y.setApplicationKey(l), a.initialize(l, e) && (o(t), a.getDevice().getTrackingStatus() && (c.start(), i()), g.info(v.Log.InitialisationSuccessful, {
                application: l,
                version: y.getSdkVersion()
            }), p = !0, r())
        }

        e.prototype.initialize = function (e, t, r) {
            if (this.canStarted()) {
                var n = e[v.ApplicationKey];
                if (DTDHelpers.isNullOrEmpty(n)) return g.info(v.Log.NullOrEmpty, {name: v.ApplicationKey}), void g.info(v.Log.NotInitialisation);
                p ? n !== l ? g.info(v.Log.ReInitialisationFail, {
                    application: l,
                    version: y.getSdkVersion()
                }) : function (e) {
                    var t = e[v.DeviceId] || a.getApp().getDummyId();
                    a.getDevice().getDeviceId() !== t && (a.generateUserEngagement(), a.initialize(l, e) && (a.getDevice().getTrackingStatus() && o(), g.info(v.Log.ReInitialisationSuccessful, {
                        application: l,
                        version: y.getSdkVersion()
                    })))
                }(e) : (b(e, t, r), d.start())
            } else g.info(v.Log.InitialisationFail, {application: l, version: y.getSdkVersion()})
        };
        var w = function (e, t) {
            e === DTDActionType.GenerateEvent && function (e) {
                a.generateEvent(e) && k()
            }(t), e === DTDActionType.SetUserId && function (e) {
                var t = a.getUser().getPrimaryId();
                a.registerUser(e) ? a.getUser().getPrimaryId() !== t ? (g.info(v.Log.SetUserSuccessful, {name: a.getUser().getUserId()}), o()) : g.info(v.Log.RenameUserSuccessful, {
                    oldName: a.getUser().getPrevUserId(),
                    newName: a.getUser().getUserId()
                }) : g.info(v.Log.SetUserSuccessfulNotValid, {name: a.getUser().getUserId()})
            }(t), e === DTDActionType.SetCurrentLevel && a.setCurrentLevel(t), e === DTDActionType.SetCurrencyAccrual && a.setCurrencyAccrual(t), e === DTDActionType.SetAppVersion && (a.setAppVersion(t), i()), e === DTDActionType.ReplaceUserId && a.replaceUserId(t), e === DTDActionType.SetProgressionEvent && a.progressionEvent(t), e === DTDActionType.RemoteConfig && a.executeRemoteConfig(t), e === DTDActionType.People && a.setUserCard(t)
        };
        e.prototype.onReceiveMessage = function (e, t) {
            w(e, t)
        }, e.prototype.isConnected = function () {
            return p
        }, e.prototype.getterCommand = function (e, t) {
            return function (e, t) {
                return e === DTDActionType.GetCurrentLevel ? a.getUser().getCurrentLevel() : e === DTDActionType.GetDeviceId ? a.getDevice().getDeviceId() : e === DTDActionType.GetAppVersion ? a.getApp().getVersion() : e === DTDActionType.GetUserId ? a.getUser().getUserId() : e === DTDActionType.GetSdkVersion ? y.getSdkVersion() : e === DTDActionType.People ? a.getUserCard(t) : e === DTDActionType.GetTrackingStatus ? a.getDevice().getTrackingStatus() : e === DTDActionType.RemoteConfig ? a.getDataRemoteConfig(t) : null
            }(e, t)
        };
        var k = function () {
            a.needSendEvent() && A()
        }, E = function () {
            a.canSendEvent() && A()
        }, A = function () {
            u.sendEvents()
        };
        return e.prototype.sendEvents = function () {
            E()
        }, e
    }(), DTDAnalyticsProxy = function () {
        "use strict";

        function e() {
            i = new DTDClient, a = new DeferredMessageManager
        }

        function s(e, t) {
            i.canStarted && (i.isConnected() ? (r.debug("sendMessage command: ".concat(e, ", arguments: ").concat(JSON.stringify(t))), i.onReceiveMessage(e, t)) : a.add(e, t))
        }

        function n(e, t) {
            if (i.canStarted) {
                if (i.isConnected() || -1 < [c.GetSdkVersion].indexOf(e)) return i.getterCommand(e, t);
                r.info(l.Log.NotInitialisation)
            }
        }

        function o() {
            a.clear()
        }

        var t, i, a, r = DTDLogger.getInstance(), u = DTDEventKeys, c = DTDActionType, l = DTDKeys, d = DTDHelpers,
            f = d.hasKey, p = d.forEachObj;
        e.prototype.initialize = function (e, t, r) {
            var n;
            i.initialize(_objectSpread((_defineProperty(n = {}, l.ApplicationKey, e), _defineProperty(n, l.DeviceId, t), _defineProperty(n, l.PrevDeviceId, r), n), a.getBundle()), a.get(), o)
        }, e.prototype.socialNetworkConnect = function (e) {
            var t;
            s(c.GenerateEvent, (_defineProperty(t = {}, u.Code, u.SocialConnect.Code), _defineProperty(t, u.SocialConnect.SocialNetwork, e), t))
        }, e.prototype.setCurrentLevel = function (e) {
            s(c.SetCurrentLevel, _defineProperty({}, u.Level, e))
        }, e.prototype.getCurrentLevel = function () {
            return n(c.GetCurrentLevel)
        }, e.prototype.getTrackingStatus = function () {
            return n(c.GetTrackingStatus)
        }, e.prototype.socialNetworkConnect = function (e) {
            var t;
            s(c.GenerateEvent, (_defineProperty(t = {}, u.Code, u.SocialConnect.Code), _defineProperty(t, u.SocialConnect.SocialNetwork, e), t))
        }, e.prototype.tutorial = function (e) {
            var t;
            s(c.GenerateEvent, (_defineProperty(t = {}, u.Code, u.Tutorial.Code), _defineProperty(t, u.Tutorial.Step, e), t))
        }, e.prototype.setUserId = function (e) {
            s(c.SetUserId, _defineProperty({}, l.UserId, e))
        }, e.prototype.setTrackingAvailability = function (e) {
            var t;
            s(c.GenerateEvent, (_defineProperty(t = {}, u.Code, u.TrackingAvailability.Code), _defineProperty(t, u.TrackingAvailability.TrackingAllowed, e), t))
        }, e.prototype.socialNetworkPost = function (e, t) {
            var r;
            s(c.GenerateEvent, (_defineProperty(r = {}, u.Code, u.SocialPost.Code), _defineProperty(r, u.SocialPost.SocialNetwork, e), _defineProperty(r, u.SocialPost.PostReason, t), r))
        }, e.prototype.realCurrencyPayment = function (e, t, r, n) {
            var o;
            s(c.GenerateEvent, (_defineProperty(o = {}, u.Code, u.RealCurrencyPayment.Code), _defineProperty(o, u.RealCurrencyPayment.Price, t), _defineProperty(o, u.RealCurrencyPayment.OrderId, e), _defineProperty(o, u.RealCurrencyPayment.ProductId, r), _defineProperty(o, u.RealCurrencyPayment.CurrencyCode, n), o))
        }, e.prototype.getUserId = function () {
            return n(c.GetUserId)
        }, e.prototype.getAppVersion = function () {
            return n(c.GetAppVersion)
        }, e.prototype.getDeviceId = function () {
            return n(c.GetDeviceId)
        }, e.prototype.getSdkVersion = function () {
            return n(c.GetSdkVersion)
        }, e.prototype.setAppVersion = function (e) {
            s(c.SetAppVersion, _defineProperty({}, u.AppVersion, e))
        }, e.prototype.realCurrencyPayment = function (e, t, r, n) {
            var o;
            s(c.GenerateEvent, (_defineProperty(o = {}, u.Code, u.RealCurrencyPayment.Code), _defineProperty(o, u.RealCurrencyPayment.Price, t), _defineProperty(o, u.RealCurrencyPayment.OrderId, e), _defineProperty(o, u.RealCurrencyPayment.ProductId, r), _defineProperty(o, u.RealCurrencyPayment.CurrencyCode, n), o))
        }, e.prototype.customEvent = function (e, t) {
            var r;
            s(c.GenerateEvent, (_defineProperty(r = {}, u.Code, u.CustomEvent.Code), _defineProperty(r, u.CustomEvent.Name, e), _defineProperty(r, u.Parameters, t), r))
        }, e.prototype.getUserCard = function (e) {
            return n(c.People, _defineProperty({}, l.Action, e))
        };

        function g(e) {
            var t = {}, r = e.length;
            return 0 < r && (1 === r && d.isObject(e[0]) ? t = e[0] : t[e[0]] = e[1]), t
        }

        return e.prototype.virtualCurrencyPayment = function (e, t, r, n) {
            var o, i = {}, a = n.length;
            0 < a && (1 === a && d.isObject(n[0]) ? i = n[0] : i[n[1]] = n[0]), s(c.GenerateEvent, (_defineProperty(o = {}, u.Code, u.VirtualCurrencyPayment.Code), _defineProperty(o, u.VirtualCurrencyPayment.PurchaseId, e), _defineProperty(o, u.VirtualCurrencyPayment.PurchaseType, t), _defineProperty(o, u.VirtualCurrencyPayment.PurchaseAmount, r), _defineProperty(o, u.VirtualCurrencyPayment.PurchasePrice, i), o))
        }, e.prototype.currencyAccrual = function (e, t, r, n) {
            var o;
            s(c.SetCurrencyAccrual, (_defineProperty(o = {}, u.CurrencyAccrual.CurrencyName, e), _defineProperty(o, u.CurrencyAccrual.CurrencyAmount, t), _defineProperty(o, u.CurrencyAccrual.AccrualType, n), _defineProperty(o, u.CurrencyAccrual.CurrencySource, r), o))
        }, e.prototype.levelUp = function (e, t, r, n, o) {
            var i;
            s(c.GenerateEvent, (_defineProperty(i = {}, u.Code, u.LevelUp.Code), _defineProperty(i, u.LevelUp.Level, e), _defineProperty(i, u.LevelUp.Bought, o), _defineProperty(i, u.LevelUp.Balance, t), _defineProperty(i, u.LevelUp.Spent, r), _defineProperty(i, u.LevelUp.Earned, n), i))
        }, e.prototype.replaceUserId = function (e, t) {
            var r;
            s(c.ReplaceUserId, (_defineProperty(r = {}, u.UserId, t), _defineProperty(r, u.PrevUserId, e), r))
        }, e.prototype.referrer = function () {
            var e = 0 < arguments.length && void 0 !== arguments[0] ? arguments[0] : {};
            s(c.GenerateEvent, _objectSpread(_defineProperty({}, u.Code, u.Referrer.Code), e))
        }, e.prototype.progressionEvent = function (e, t) {
            var r = function (r, e) {
                var n = {};
                return p(function (e, t) {
                    f(e, r) && (n[e] = r[e])
                }, e), n
            }(2 < arguments.length && void 0 !== arguments[2] ? arguments[2] : {}, u.ProgressionEvent);
            r[u.Code] = u.ProgressionEvent.Code, r[l.State] = e, r[u.ProgressionEvent.Name] = t, s(c.SetProgressionEvent, r)
        }, e.prototype.sendEvents = function () {
            i.sendEvents()
        }, e.prototype.setPeopleProperties = function () {
            for (var e, t = arguments.length, r = new Array(t), n = 0; n < t; n++) r[n] = arguments[n];
            var o = g(r);
            s(c.People, (_defineProperty(e = {}, l.Action, l.People.Action.Set), _defineProperty(e, l.People.Properties, o), e))
        }, e.prototype.setPeoplePropertiesOnce = function () {
            for (var e, t = arguments.length, r = new Array(t), n = 0; n < t; n++) r[n] = arguments[n];
            var o = g(r);
            s(c.People, (_defineProperty(e = {}, l.Action, l.People.Action.SetOnce), _defineProperty(e, l.People.Properties, o), e))
        }, e.prototype.unsetPeopleProperties = function (e) {
            var t;
            s(c.People, (_defineProperty(t = {}, l.Action, l.People.Action.Unset), _defineProperty(t, l.People.Properties, e), t))
        }, e.prototype.deletePople = function () {
            s(c.People, _defineProperty({}, l.Action, l.People.Action.Clear))
        }, e.prototype.getRemoteConfig = function (e, t) {
            var r;
            return n(c.RemoteConfig, (_defineProperty(r = {}, l.Action, e), _defineProperty(r, l.RemoteConfig.ConfigKey, t), r))
        }, e.prototype.setRemoteConfig = function (e, t) {
            var r;
            s(c.RemoteConfig, (_defineProperty(r = {}, l.Action, e), _defineProperty(r, l.Properties, t), r))
        }, e.prototype.fetchExperiments = function (e) {
            var t;
            s(c.RemoteConfig, (_defineProperty(t = {}, l.Action, l.RemoteConfig.Action.Fetch), _defineProperty(t, l.RemoteConfig.Callback, e), t))
        }, e.prototype.activateFetchedExperiments = function () {
            s(c.RemoteConfig, _defineProperty({}, l.Action, l.RemoteConfig.Action.Activate))
        }, {
            getInstance: function () {
                return t = t || new e
            }
        }
    }();
    DTDRemoteConfig = function () {
        var t;

        function e(e) {
            t = e, Object.defineProperty(this, "defaults", {
                get: function () {
                    return t.getRemoteConfig(DTDKeys.RemoteConfig.Action.GetDefaultsConfig)
                }, set: function (e) {
                    t.setRemoteConfig(DTDKeys.RemoteConfig.Action.SetDefaultsConfig, e)
                }
            }), Object.defineProperty(this, "remote", {
                get: function () {
                    return t.getRemoteConfig(DTDKeys.RemoteConfig.Action.GetRemoteConfig)
                }
            }), Object.defineProperty(this, "abtest", {
                get: function () {
                    return t.getRemoteConfig(DTDKeys.RemoteConfig.Action.GetABTest)
                }
            })
        }

        return e.prototype.configValue = function (e) {
            return t.getRemoteConfig(DTDKeys.RemoteConfig.Action.GetConfigKey, e)
        }, e.prototype.fetch = function (e) {
            t.fetchExperiments(e)
        }, e.prototype.activateFetched = function () {
            t.activateFetchedExperiments(), t.sendEvents()
        }, e
    }(), DTDProfile = function () {
        var t;

        function e(e) {
            t = e, Object.defineProperty(this, "name", {
                get: function () {
                    return t.getUserCard(DTDKeys.People.Name)
                }, set: function (e) {
                    t.setPeopleProperties(DTDKeys.People.Name, e)
                }
            }), Object.defineProperty(this, "email", {
                get: function () {
                    return t.getUserCard(DTDKeys.People.Email)
                }, set: function (e) {
                    t.setPeopleProperties(DTDKeys.People.Email, e)
                }
            }), Object.defineProperty(this, "photo", {
                get: function () {
                    return t.getUserCard(DTDKeys.People.Photo)
                }, set: function (e) {
                    t.setPeopleProperties(DTDKeys.People.Photo, e)
                }
            }), Object.defineProperty(this, "phone", {
                get: function () {
                    return t.getUserCard(DTDKeys.People.Phone)
                }, set: function (e) {
                    t.setPeopleProperties(DTDKeys.People.Phone, e)
                }
            }), Object.defineProperty(this, "gender", {
                get: function () {
                    return t.getUserCard(DTDKeys.People.Gender)
                }, set: function (e) {
                    t.setPeopleProperties(DTDKeys.People.Gender, e)
                }
            }), Object.defineProperty(this, "age", {
                get: function () {
                    return t.getUserCard(DTDKeys.People.Age)
                }, set: function (e) {
                    t.setPeopleProperties(DTDKeys.People.Age, e)
                }
            }), Object.defineProperty(this, "cheater", {
                get: function () {
                    return t.getUserCard(DTDKeys.People.Cheater)
                }, set: function (e) {
                    t.setPeopleProperties(DTDKeys.People.Cheater, e)
                }
            })
        }

        return e.prototype.unset = function (e) {
            t.unsetPeopleProperties(e)
        }, e.prototype.set = function () {
            var e;
            (e = t).setPeopleProperties.apply(e, arguments)
        }, e.prototype.setOnce = function () {
            var e;
            (e = t).setPeoplePropertiesOnce.apply(e, arguments)
        }, e.prototype.deleteUser = function () {
            t.deletePople()
        }, e
    }();
    var DTDAnalitics = function () {
        var t = DTDLogger.getInstance(), r = DTDApplicationInfo.getInstance(), a = DTDAnalyticsProxy.getInstance(),
            n = new DTDRemoteConfig(a);

        function e() {
            var e = 0 < arguments.length && void 0 !== arguments[0] && arguments[0];
            r.isTestingBuild = e, Object.defineProperty(this, "logLevel", {
                get: function () {
                    return t.getLogLevel()
                }, set: function (e) {
                    t.setLogLevel(e)
                }
            }), Object.defineProperty(this, "sdkVersion", {
                get: function () {
                    return a.getSdkVersion()
                }
            }), Object.defineProperty(this, "currentLevel", {
                get: function () {
                    return a.getCurrentLevel()
                }, set: function (e) {
                    a.setCurrentLevel(e)
                }
            }), Object.defineProperty(this, "userId", {
                get: function () {
                    return a.getUserId()
                }, set: function (e) {
                    a.setUserId(e)
                }
            }), Object.defineProperty(this, "deviceId", {
                get: function () {
                    return a.getDeviceId()
                }
            }), Object.defineProperty(this, "sessionLength", {
                get: function () {
                }
            }), Object.defineProperty(this, "sessionStarted", {
                get: function () {
                }
            }), Object.defineProperty(this, "appVersion", {
                get: function () {
                    return a.getAppVersion()
                }, set: function (e) {
                    a.setAppVersion(e)
                }
            }), Object.defineProperty(this, "trackingAvailability", {
                get: function () {
                    return a.getTrackingStatus()
                }, set: function (e) {
                    a.setTrackingAvailability(e)
                }
            }), Object.defineProperty(this, "user", {
                get: function () {
                    return new DTDProfile(a)
                }
            }), Object.defineProperty(this, "remoteConfig", {
                get: function () {
                    return n
                }
            })
        }

        return e.prototype.initialize = function (e, t, r) {
            a.initialize(e, t, r)
        }, e.prototype.socialNetworkConnect = function (e) {
            a.socialNetworkConnect(e)
        }, e.prototype.socialNetworkPost = function (e, t) {
            a.socialNetworkPost(e, t)
        }, e.prototype.tutorial = function (e) {
            a.tutorial(e)
        }, e.prototype.realCurrencyPayment = function (e, t, r, n) {
            a.realCurrencyPayment(e, t, r, n)
        }, e.prototype.virtualCurrencyPayment = function (e, t, r) {
            for (var n = arguments.length, o = new Array(3 < n ? n - 3 : 0), i = 3; i < n; i++) o[i - 3] = arguments[i];
            a.virtualCurrencyPayment(e, t, r, o)
        }, e.prototype.customEvent = function (e) {
            var t = 1 < arguments.length && void 0 !== arguments[1] ? arguments[1] : [];
            a.customEvent(e, t)
        }, e.prototype.levelUp = function (e, t, r, n, o) {
            a.levelUp(e, t, r, n, o)
        }, e.prototype.currencyAccrual = function (e, t, r, n) {
            a.currencyAccrual(e, t, r, n)
        }, e.prototype.replaceUserId = function (e, t) {
            a.replaceUserId(e, t)
        }, e.prototype.startProgressionEvent = function (e, t) {
            a.progressionEvent(DTDProgressionState.Start, e, t)
        }, e.prototype.finishProgressionEvent = function (e, t) {
            a.progressionEvent(DTDProgressionState.Finish, e, t)
        }, e.prototype.referrer = function (e) {
            a.referrer(e)
        }, e.prototype.sendBufferedEvents = function () {
            a.sendEvents()
        }, e.prototype.setUrl = function (e) {
            r.isTestingBuild && (r.apiUrl = e)
        }, e.prototype.setTestName = function (e) {
            r.isTestingBuild && (r.testName = e)
        }, e
    }();
    window.devtodev = new DTDAnalitics();
})(this);
