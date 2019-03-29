/****************************************************************************
 Copyright (c) 2017-2018 Xiamen Yaji Software Co., Ltd.

 http://www.cocos.com

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated engine source code (the "Software"), a limited,
 worldwide, royalty-free, non-assignable, revocable and non-exclusive license
 to use Cocos Creator solely to develop games on your target platforms. You shall
 not use Cocos Creator software for developing other software or tools that's
 used for developing games. You are not granted to publish, distribute,
 sublicense, and/or sell copies of Cocos Creator.

 The software or tools in this License Agreement are licensed, not sold.
 Xiamen Yaji Software Co., Ltd. reserves all rights not expressly granted to you.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 THE SOFTWARE.
 ****************************************************************************/

#define USE_AUDIO 1
#define USE_NET_WORK 1


#include "cocos/scripting/js-bindings/manual/jsb_module_register.hpp"
#include "cocos/scripting/js-bindings/jswrapper/SeApi.h"

#include "cocos/scripting/js-bindings/auto/jsb_cocos2dx_extension_auto.hpp"
#include "cocos/scripting/js-bindings/auto/jsb_cocos2dx_network_auto.hpp"
#include "cocos/scripting/js-bindings/auto/jsb_renderer_auto.hpp"
#include "cocos/scripting/js-bindings/auto/jsb_gfx_auto.hpp"
#include "cocos/scripting/js-bindings/auto/jsb_cocos2dx_auto.hpp"

#include "cocos/scripting/js-bindings/manual/jsb_global.h"
#include "cocos/scripting/js-bindings/manual/jsb_node.hpp"
#include "cocos/scripting/js-bindings/manual/jsb_conversions.hpp"
#include "cocos/scripting/js-bindings/manual/jsb_gfx_manual.hpp"
#include "cocos/scripting/js-bindings/manual/jsb_renderer_manual.hpp"
#include "cocos/scripting/js-bindings/manual/jsb_opengl_manual.hpp"
#include "cocos/scripting/js-bindings/manual/jsb_platform.h"
#include "cocos/scripting/js-bindings/manual/jsb_cocos2dx_manual.hpp"

#if USE_NET_WORK
#include "cocos/scripting/js-bindings/manual/jsb_xmlhttprequest.hpp"
#include "cocos/scripting/js-bindings/manual/jsb_websocket.hpp"
#include "cocos/scripting/js-bindings/manual/jsb_socketio.hpp"
#include "cocos/scripting/js-bindings/manual/jsb_cocos2dx_network_manual.h"
#endif // USE_NET_WORK

#if USE_AUDIO
#include "cocos/scripting/js-bindings/auto/jsb_cocos2dx_audioengine_auto.hpp"
#endif

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS || CC_TARGET_PLATFORM == CC_PLATFORM_MAC)
#include "cocos/scripting/js-bindings/manual/JavaScriptObjCBridge.h"
#endif

#if (CC_TARGET_PLATFORM == CC_PLATFORM_ANDROID)
#include "cocos/scripting/js-bindings/manual/JavaScriptJavaBridge.h"
#endif

#include "cocos2d.h"

using namespace cocos2d;

bool jsb_register_all_modules()
{
    se::ScriptEngine* se = se::ScriptEngine::getInstance();

    se->addBeforeInitHook([](){
        JSBClassType::init();
    });

    se->addBeforeCleanupHook([se](){
        se->garbageCollect();
        PoolManager::getInstance()->getCurrentPool()->clear();
        se->garbageCollect();
        PoolManager::getInstance()->getCurrentPool()->clear();
    });

    se->addRegisterCallback(jsb_register_global_variables);
    se->addRegisterCallback(JSB_register_opengl);
    se->addRegisterCallback(register_all_gfx);
    se->addRegisterCallback(jsb_register_gfx_manual);
    se->addRegisterCallback(register_all_renderer);
    se->addRegisterCallback(jsb_register_renderer_manual);
    se->addRegisterCallback(register_all_cocos2dx);
    se->addRegisterCallback(register_all_cocos2dx_manual);
    se->addRegisterCallback(register_platform_bindings);
    se->addRegisterCallback(register_all_cocos2dx_extension);

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS || CC_TARGET_PLATFORM == CC_PLATFORM_MAC)
    se->addRegisterCallback(register_javascript_objc_bridge);
#endif

#if (CC_TARGET_PLATFORM == CC_PLATFORM_ANDROID)
    se->addRegisterCallback(register_javascript_java_bridge);
#endif

#if USE_AUDIO
    se->addRegisterCallback(register_all_cocos2dx_audioengine);
#endif


#if USE_NET_WORK
    se->addRegisterCallback(register_all_cocos2dx_network);
    se->addRegisterCallback(register_all_cocos2dx_network_manual);
    se->addRegisterCallback(register_all_xmlhttprequest);
    se->addRegisterCallback(register_all_websocket);
    se->addRegisterCallback(register_all_socketio);
#endif

    se->addAfterCleanupHook([](){
        PoolManager::getInstance()->getCurrentPool()->clear();
        JSBClassType::destroy();
    });
    return true;
}
