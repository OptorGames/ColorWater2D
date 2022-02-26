//
//  CASUCallback.m
//  CASUnityPlugin
//
//  Copyright © 2020 Clever Ads Solutions. All rights reserved.
//

#import "CASUCallback.h"
#import "CASUPluginUtil.h"
#if __has_include(<FirebaseAnalytics/FIRAnalytics.h>)
#import <FirebaseAnalytics/FIRAnalytics.h>
#endif

@implementation CASUCallback
{
    BOOL withComplete;
}

- (id)initWithComplete:(BOOL)complete {
    self = [super init];
    if (self) {
        withComplete = complete;
    }
    return self;
}

- (void)willShownWithAd:(id<CASStatusHandler>)adStatus {
    [CASUPluginUtil onAdsWillPressent];
    if (self.client) {
        if (self.willOpeningCallback) {
            self.willOpeningCallback(self.client,
                                     [[CASNetwork values] indexOfObject:adStatus.network],
                                     adStatus.cpm,
                                     adStatus.priceAccuracy);
        }
    }
}

- (void)didShowAdFailedWithError:(NSString *)error {
    [CASUPluginUtil onAdsDidClosed];
    if (self.didShowFailedCallback) {
        if (self.client) {
            self.didShowFailedCallback(self.client, [error cStringUsingEncoding:NSUTF8StringEncoding]);
        }
    }
}

- (void)didCompletedAd {
    if (!withComplete) {
        return;
    }
    if (self.didCompleteCallback) {
        if (self.client) {
            self.didCompleteCallback(self.client);
        }
    }
}

- (void)didClickedAd {
    if (self.didClickCallback) {
        if (self.client) {
            self.didClickCallback(self.client);
        }
    }
}

- (void)didClosedAd {
    // Escape from callback when App on background. Not supported for Cross Promo logic.
    //    extern bool _didResignActive;
    //    if (_didResignActive) {
    //        // We are in the middle of the shutdown sequence, and at this point unity runtime is already destroyed.
    //        // We shall not call unity API, and definitely not script callbacks, so nothing to do here
    //        return;
    //    }

    [CASUPluginUtil onAdsDidClosed];
    if (self.didClosedCallback) {
        if (self.client) {
            self.didClosedCallback(self.client);
        }
    }
}

- (void)log:(NSString *)eventName:(NSDictionary<NSString *, id> *)map {
    #if __has_include(<FirebaseAnalytics/FIRAnalytics.h>)
    [FIRAnalytics logEventWithName:eventName parameters:map];
    #else
    NSLog(@"[CAS] Framework bridge cant find Firebase Analytics");
    #endif
}

- (UIViewController *)viewControllerForPresentingAppReturnAd {
    return [CASUPluginUtil unityGLViewController];
}

@end
